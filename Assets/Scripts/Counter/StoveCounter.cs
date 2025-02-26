using System;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt,
    }

    private AudioSource audioSource;



    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;


    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    private void Start()
    {
        state = State.Idle;
        this.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, OnStateChangedEventArgs e)
    {
        if (state != State.Idle)
        {
            if (!audioSource.isPlaying)  // Prevent overlapping sounds
            {
                audioSource.Play();  // Plays from the stove's position
            }
        }
        else
        {
            audioSource.Stop();  // Stops when frying ends
        }
    }

    private void Update()
    {
        if (HasFoodObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax

                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //has been fried
                        GetFoodObject().DestroySelf();
                        FoodObject.SpawnFoodObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        fryingRecipeSO = GetFryingRecipeSOWithInput(GetFoodObject().GetFoodObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        ProgressNormalized = burningTimer / fryingRecipeSO.fryingTimerMax

                    });
                    if (burningTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //has been fried
                        GetFoodObject().DestroySelf();
                        FoodObject.SpawnFoodObject(fryingRecipeSO.output, this);
                        Debug.Log(GetFoodObject());
                        state = State.Burnt;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                        {
                            ProgressNormalized = 0f

                        });
                    }
                    break;
                case State.Burnt:
                    break;
            }
        }

    }
    public override void Interact(Player player)
    {
        if (!HasFoodObject())   //no food object on counter
        {

            if (player.HasFoodObject())
            {
                if (HasRecipeWithInput(player.GetFoodObject().GetFoodObjectSO()))
                {
                    player.GetFoodObject().SetFoodObjectParent(this);
                    this.GetFoodObject().GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;


                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetFoodObject().GetFoodObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        ProgressNormalized = fryingTimer/fryingRecipeSO.fryingTimerMax

                    });
                }

            }
            else
            {
                //player not carrying anything
            }
        }
        else //if there is food object on counter
        {
            //hve food object here
            if (player.HasFoodObject())
            {
                //Player carrying something
                if (player.GetFoodObject().TryGetPlate(out PlateFoodObject plateFoodObject))
                {
                    //player holding a plate
                    if (plateFoodObject.TryAddIngredient(GetFoodObject().GetFoodObjectSO()))
                    //if succeed add ingredient, can destroy itself
                    {
                        GetFoodObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                        {
                            ProgressNormalized = 0f

                        });
                    }

                }
            }
            else
            {
                //player not carrying anything
                GetFoodObject().SetFoodObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                {
                    ProgressNormalized = 0f

                });
            }
        }
    }

    private bool HasRecipeWithInput(FoodObjectSO inputFoodObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputFoodObjectSO);
        return fryingRecipeSO != null;
    }
    private FoodObjectSO GetOutputForInput(FoodObjectSO inputFoodObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputFoodObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(FoodObjectSO inputFoodObjectSO)
    {
        //check if inputFoodObjectSO exist in any of the FryingRecipeSOArr inputs

        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputFoodObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

}
