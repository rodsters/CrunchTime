using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;

// Made by Jasper Fadden
// This is an upgraded draft of Rainbow Man's player controller. The movement code utilizes the ADSR system often seen in music that our professor described in a lecture. This allows smooth slow-down and speed-up physics that make movement feel much less rigid. The controller also implements some rudimentary animation (having the player turn to face the mouse), player statistics like shooting speed and inaccuracy, and more.

// Credits to Josh McCoy for much of the code implementing Attack-Decay-Sustain-Release movement.

public class PlayerController : MonoBehaviour
{
    // Movement speed for the player. It is serialized and has a setter function for upgrades.
    [SerializeField] float speed = 10.0f;
    // This is used to store the normal speed before setting it to something else (ex. 0 when dashing).
    float normalSpeed;

    // This is the prefab the player shoors.
    [SerializeField] public ProjectileController ProjectilePrefab;

    // Max vs current health. These are to be changes or accessed with several dedicated getter/sett functions. 
    [SerializeField] float maxHealth = 40.0f;
    float currentHealth;

    Rigidbody2D rigidbody2d;
    SpriteRenderer sprite;
    TrailRenderer trail;
    // These two variables are used to store what direction the player is moving in. Right/Upwards are positive, Left/Down negative.
    float horizontal;
    float vertical;
    // This is used to temporarily store the player position before translating them.
    Vector3 position;

    // Used to store what angle clockwise the player's mouse is relative to the player.
    float angle;
    Vector3 mousePosition;
    // This variable stores the cooldown between shots for the player. The player can shoot 1 / FireRate shots per second.
    // Set to public for upgrades to access it.
    [SerializeField] public float FireRate = 0.3f;
    // This is a timer that is set to fire rate after shooting but decrements by Time.deltaTime every frame.
    private float FireRateTimer = 0.0f;
    private float distanceMouseIs = 0.0f;
    private Vector3 Direction3D;

    // Not sure whether or not this should be a thing, but it could be fun for upgrades (maybe a minigun one that adds
    // to inaccuracy but gives a huge fire-rate, or one that sets inaccuracy to be 0).
    // Set to public for projectiles to access it, and it has a setter function.
    [SerializeField] static public float inaccuracy = 2.25f;


    // This is the amount of damage points each projectile deals. Like inaccuracy, it is accessed by the projectile prefab.
    [SerializeField] static public float damage = 3.35f;


    // To avoid counter-strike style bunnyhop shennanigans, decay and sustain are basically ignored and set to normal speed.
    [SerializeField] private float AttackDuration = 0.45f;
    [SerializeField] private AnimationCurve Attack;

    [SerializeField] private float DecayDuration = 1.0f;
    [SerializeField] private AnimationCurve Decay;

    [SerializeField] private float SustainDuration = 1.0f;
    [SerializeField] private AnimationCurve Sustain;

    [SerializeField] private float ReleaseDuration = 0.64f;
    [SerializeField] private AnimationCurve Release;

    // Two versions must be made for most variables in order to bring the code from 1D to 2D
    private float HAttackTimer;
    private float HDecayTimer;
    private float HSustainTimer;
    private float HReleaseTimer;

    private float VAttackTimer;
    private float VDecayTimer;
    private float VSustainTimer;
    private float VReleaseTimer;

    // These custom made timers are to prevent the player from sliding when they stop themself by pressing contrasting movement keys
    private float HDualReleaseTimer = 0.0f;
    private float VDualReleaseTimer = 0.0f;
    private float DualReleaseThreshold = 0.1f;

    // This timer controls whether or not Rainbowman currently has invulnerability frames active.
    [SerializeField] private float invulnerabilityTime = 1.0f;
    private float InvulnerabilityTimer = 0.0f;

    // This timer controls the speed for HP regeneration. The rate is serialized and has a setter function in the case of upgrades.
    [SerializeField] private float regenTimerRate = 2.0f;
    private float RegenTimer = 0;
    private bool canRegen = true;


    // This timer checks for debuffs from negative time. It has a throttle to prevent lag.
    [SerializeField] private float debuffTimerThrottle = 2.0f;
    private float DebuffTimer = 0;
    float currentTime = 180;
    bool hasRegenDebuff = false;
    bool hasFiringDebuff = false;

    [SerializeField]
    private float damageFromHazards = 7.5f;

    // This timer controls the time for a player's dash. Time and speed are serialized and have setter functions for upgrades.
    // Note that the player cannot dash again until DashTimer is less than (-dashCooldown * 3), 
    // something designed to prevent the player from infinitely dashing and therefore being completely invulnerable.
    [SerializeField] private float dashTimeLength = 0.25f;
    [SerializeField] private float dashCooldown = 0.25f;
    private float DashTimer = 0;
    private bool isDashing = false;
    [SerializeField] private float dashSpeed = 22.5f;
    // Used to temporarily store the angle chosen at the beginning of a dash.
    Vector3 dashAngleVector;
    private float dashAngle;

    // There are five possible phases the player can be in, each one modifies speed depending on how long in the phase the player is.
    private enum Phase { Attack, Decay, Sustain, Release, None };
    private Phase CurrentPhaseVertical;
    private Phase CurrentPhaseHorizontal;

    private GameObject gameManager;
    private Timer timer;
    private GameObject BlindnessEffect;

    private SoundManager soundSystem;
    private bool playingNegativeTimeMusic;
    
    // Animation variables; Harrison
    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update.
    void Start()
    {
        // Easily access components with variables.
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        CurrentPhaseVertical = Phase.None;
        CurrentPhaseHorizontal = Phase.None;
        trail = GetComponent<TrailRenderer>();
        gameManager = GameObject.Find("GameManager");
        timer = gameManager.GetComponent<Timer>();
        // Note the .instance, The first SoundManager created always puts itself in this static variable as the main music player.
        soundSystem = SoundManager.instance;
        soundSystem.PlayMusicTrack("CrunchTime");

        // This set of two property changes slightly enhances collision so the player doesn't clip into walls.
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;


        // Blindness is scrapped for now because it obliterates the game's FPS

        // This is done to not crowd the editor but have the overlay in the right position upon playing.
        //BlindnessEffect = GameObject.Find("Blind");
        //BlindnessEffect.transform.position = this.transform.position;
        //BlindnessEffect.SetActive(false);

        currentHealth = maxHealth;
        RegenTimer = regenTimerRate;
        DashTimer = (-3 * dashCooldown);
        normalSpeed = speed;
        trail.emitting = false;
        trail.widthMultiplier = 0.75f;
    }

    // Update is called once per frame.
    void Update()
    {
       // Debug.Log( (int)(1f / Time.unscaledDeltaTime) );
        // Decrement all timers in real timer every Update() frame (like regneration, invulnerability, and movement stage timers).
        DecrementTimers();

        // This gargantuan function accurately get's the player's direction respecting stages for FixedUpdate's translations.
        GetMovementDirection();

        // This code is for shooting. The player will be able to shoot by holding down either
        // of the two buttons, repeating fire every time the FireRateTimer reaches 0.
        if ( (Input.GetButton("Jump") || Input.GetButton("Fire1")) && FireRateTimer <= 0 )
        {
            soundSystem.PlaySoundEffect("Shooting");
            FireRateTimer = FireRate;
            // Instantiates projectile where weapon is.
            Vector2 direction = mousePosition - transform.position;
            Vector2 location = direction.normalized;
            distanceMouseIs = direction.magnitude;
            // This is used to set the spawnpoint for close porjectiles.
            Vector3 Direction3D = new Vector3(direction.x * 0.9f, direction.y * 0.9f, 0);

            if (distanceMouseIs > 1.005)
            {
                Instantiate(ProjectilePrefab, new Vector3(gameObject.transform.position.x + location.x, gameObject.transform.position.y + location.y, gameObject.transform.position.z), transform.rotation);
            }
            else
            {
                // This is a quick fix the bug where the player shoots themself (its funny but its a problem when enemies get close)
                Instantiate(ProjectilePrefab, new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y,
                    gameObject.transform.position.z) + Direction3D, transform.rotation);
            }
        }
        // Make clicking a tiny bit faster (again, unsure if this is what we should do. If it feels weird, delete it).
        // IIRC, this is done by many games so it's super unlikely that the player clicks and feels it didn't register.
        else if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && ( FireRateTimer - (FireRate/2.75f) ) <= 0)
        {
            soundSystem.PlaySoundEffect("Shooting");
            FireRateTimer = FireRate;
            Vector2 direction = mousePosition - transform.position;
            Vector2 location = direction.normalized;
            distanceMouseIs = direction.magnitude;
            // This is used to set the spawnpoint for close porjectiles.
            Vector3 Direction3D = new Vector3(direction.x * 0.9f, direction.y * 0.9f, 0);

            if (distanceMouseIs > 1.005)
            {
                Instantiate(ProjectilePrefab, new Vector3(gameObject.transform.position.x + location.x, gameObject.transform.position.y + location.y, gameObject.transform.position.z), transform.rotation);
            }
            else
            {
                // This is a quick fix the bug where the player shoots themself (its funny but its a problem when enemies get close)
                Instantiate(ProjectilePrefab, new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y,
                    gameObject.transform.position.z) + Direction3D, transform.rotation);
            }
            
        }
        
        if ( (horizontal != 0.0f) || (vertical != 0.0f) )
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    // Fixed update is used for better compatibility and physics. 
    // It is important the player's position is changed here for clipping reasons.
    void FixedUpdate()
    {
        // Set the player's velocity to zero. This is to prevent continuous knockback when an enemy runs into the player.
        rigidbody2d.velocity = new Vector2(0, 0);

        // Basic sprite animation is done via flipping horizontally. It's based on where the player's mouse is, 
        // so we have to calculate things. Get the player's mouse position and RainbowMan's current direction.
        // This is used later in Update () when we get the angle for sprite flipping.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        // This is used to detect where the player should be facing.
        angle = Vector2.SignedAngle(Vector2.down, direction) + 270;

        // This code allows a dash to begin if the player has waited about a while after having dashed.
        if ((Input.GetButton("Fire2")) && (DashTimer <= (-3 * (dashCooldown))))
        {
            soundSystem.PlaySoundEffect("Dash");
            isDashing = true;
            DashTimer = dashTimeLength;
            // The player should be invulnerable during dashes to dodge projectiles or enemy attacks.
            InvulnerabilityTimer = dashTimeLength;
            
            // Dash Animation Effect.
            StartCoroutine(Faded());
            
            // We have to get a special angle for wdetermining where Rainbowman dashes.
            dashAngle = Vector2.SignedAngle(Vector2.right, direction);

            trail.emitting = true;
        }

        if (isDashing)
        {
            speed = 0;
            // Rotate rainbowman towards where they are dashing in between frames, move rainbowman that direction,
            // and ensure rainbowman goes back to default rotation afterwards when the frame is drawn.
            transform.eulerAngles = new Vector3(0, 0, dashAngle);
            transform.position += transform.right * Time.deltaTime * dashSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Stop dashing after the player has dashed for dashTimeLength seconds.
        if (DashTimer <= 0)
        {
            isDashing = false;
            speed = normalSpeed;
            trail.emitting = false;
        }

        // Regenerate health between a customizeable delay
        if (RegenTimer <= 0 && canRegen)
        {
            ChangeCurrentHealth(1.0f);
            RegenTimer = regenTimerRate;
        }

        // Check to reapply or unapply debuffs
        if (DebuffTimer <= 0)
        {
            CheckForNegativeTime();
        }

        // If on the right side of the unit circle, else on the left side.
        if ((angle) > 90 && (angle) < 270)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        // After animation code, here we move the player by the inputs calculated in the Update() function.
        if (this.CurrentPhaseHorizontal != Phase.None)
        {
            position = this.gameObject.transform.position;
            // Note position.x is changed based on "horizontal." The speed is decreased for diagonal movement.
            if (vertical != 0)
            {
                position.x += horizontal * speed * HorizontalADSREnvelope() * Time.deltaTime * (1.0f / 1.4142f);
            }
            else
            {
                position.x += horizontal * speed * HorizontalADSREnvelope() * Time.deltaTime;
            }
            gameObject.transform.position = position;

        }

        if (this.CurrentPhaseVertical != Phase.None)
        {
            position = this.gameObject.transform.position;
            // Note position.y is changed based on "vertical." Again, speed is decreased for diagonal movement.
            if (vertical != 0)
            {
                position.y += vertical * speed * VerticalADSREnvelope() * Time.deltaTime * (1.0f / 1.4142f);
            }
            else
            {
                position.y += vertical * speed * VerticalADSREnvelope() * Time.deltaTime;
            }
            gameObject.transform.position = position;
        }

        // Floats are often error ridden and slightly off, so this code ensure's the player is always properly stopped when required.
        if (horizontal < 0.01 && horizontal > -0.01)
        {
            horizontal = 0;
        }
        if (vertical < 0.01 && vertical > -0.01)
        {
            vertical = 0;
        }
    }

    // Simple function borrowed from JoshMcCoy, these resets all ADSR timers.
    private void ResetTimersHorizontal()
    {
        this.HAttackTimer = 0.0f;
        this.HDecayTimer = 0.0f;
        this.HSustainTimer = 0.0f;
        this.HReleaseTimer = 0.0f;
    }
    private void ResetTimersVertical()
    {

        this.VAttackTimer = 0.0f;
        this.VDecayTimer = 0.0f;
        this.VSustainTimer = 0.0f;
        this.VReleaseTimer = 0.0f;
    }

    // Code from JoshMcCoy, this essentially allows us to modify the value of movement depending on current state.
    // Two versions exist for horizontal and vertical movement.
    float HorizontalADSREnvelope()
    {
        float velocity = 0.0f;

        if (Phase.Attack == this.CurrentPhaseHorizontal)
        {
            velocity = this.Attack.Evaluate(this.HAttackTimer / this.AttackDuration);
            this.HAttackTimer += Time.deltaTime;
            if (this.HAttackTimer > this.AttackDuration)
            {
                this.CurrentPhaseHorizontal = Phase.Decay;
            }
        }
        else if (Phase.Decay == this.CurrentPhaseHorizontal)
        {
            velocity = this.Decay.Evaluate(this.HDecayTimer / this.DecayDuration);
            this.HDecayTimer += Time.deltaTime;
            if (this.HDecayTimer > this.DecayDuration)
            {
                this.CurrentPhaseHorizontal = Phase.Sustain;
            }
        }
        else if (Phase.Sustain == this.CurrentPhaseHorizontal)
        {
            velocity = this.Sustain.Evaluate(this.HSustainTimer / this.SustainDuration);
            this.HSustainTimer += Time.deltaTime;
        }
        else if (Phase.Release == this.CurrentPhaseHorizontal)
        {
            velocity = this.Release.Evaluate(this.HReleaseTimer / this.ReleaseDuration);
            this.HReleaseTimer += Time.deltaTime;
            if (this.HReleaseTimer > this.ReleaseDuration)
            {
                this.CurrentPhaseHorizontal = Phase.None;
            }
        }
        return velocity;
    }

    // Vertical version.
    float VerticalADSREnvelope()
    {
        float velocity = 0.0f;

        if (Phase.Attack == this.CurrentPhaseVertical)
        {
            velocity = this.Attack.Evaluate(this.VAttackTimer / this.AttackDuration);
            this.VAttackTimer += Time.deltaTime;
            if (this.VAttackTimer > this.AttackDuration)
            {
                this.CurrentPhaseVertical = Phase.Decay;
            }
        }
        else if (Phase.Decay == this.CurrentPhaseVertical)
        {
            velocity = this.Decay.Evaluate(this.VDecayTimer / this.DecayDuration);
            this.VDecayTimer += Time.deltaTime;
            if (this.VDecayTimer > this.DecayDuration)
            {
                this.CurrentPhaseVertical = Phase.Sustain;
            }
        }
        else if (Phase.Sustain == this.CurrentPhaseVertical)
        {
            velocity = this.Sustain.Evaluate(this.VSustainTimer / this.SustainDuration);
            this.VSustainTimer += Time.deltaTime;
        }
        else if (Phase.Release == this.CurrentPhaseVertical)
        {
            velocity = this.Release.Evaluate(this.VReleaseTimer / this.ReleaseDuration);
            this.VReleaseTimer += Time.deltaTime;
            if (this.VReleaseTimer > this.ReleaseDuration)
            {
                this.CurrentPhaseVertical = Phase.None;
            }
        }
        return velocity;
    }

    // To avoid crowding Update() more than it is, this function decrements all timers.
    private void DecrementTimers()
    {
        // These timers count down every frame if they are above 0
        // These "dual release timers" are designed to allow the player to stop on the spot if they hit the reverse direction 
        // and release both related movement keys. Otherwise, the player would bounce in a random direction after releasing both.
        if (HDualReleaseTimer > 0)
        {
            HDualReleaseTimer -= Time.deltaTime;
        }
        if (VDualReleaseTimer > 0)
        {
            VDualReleaseTimer -= Time.deltaTime;
        }
        // This timer is designed to enforce the player's fire rate.
        if (FireRateTimer > 0)
        {
            FireRateTimer -= Time.deltaTime;
        }
        // This timer enforces invulnerability frames so that the player isn't insta-killed by enemies attacking 60 times a second.
        if (InvulnerabilityTimer > 0)
        {
            InvulnerabilityTimer -= Time.deltaTime;
        }
        // This timer enforces health regeneration so that the player can make a come-back after mistakes.
        if (RegenTimer > 0)
        {
            RegenTimer -= Time.deltaTime;
        }

        // This timer enforces the delay and duration. Unlike other timers, it decrements to it's negative maximum.
        if (DashTimer > (-3 * dashCooldown))
        {
            DashTimer -= Time.deltaTime;
        }

        // This timer enforces the the checking of negative time for applying debuffs (as well as unapplying them).
        if (DebuffTimer > 0)
        {
            DebuffTimer -= Time.deltaTime;
        }
    }

    // This LONG chain of if statement sets get player input for WASD, and it helps implement 2D ADSR smoothly.
    // Things get much more advanced when translating things to 2D movement, so this is a long process
    private void GetMovementDirection()
    {
        // These are the 4 GetKeyDown Statements for the attack phase.
        // Note that directions and phases aren't updated every frame, only on keydown or keyup mostly.
        // This is to allow time based progression of states in case we decide to use the middle two stages.
        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontal = 1.0f;
            this.ResetTimersHorizontal();
            this.CurrentPhaseHorizontal = Phase.Attack;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontal = -1.0f;
            this.ResetTimersHorizontal();
            this.CurrentPhaseHorizontal = Phase.Attack;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            vertical = 1.0f;
            this.ResetTimersVertical();
            this.CurrentPhaseVertical = Phase.Attack;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            vertical = -1.0f;
            this.ResetTimersVertical();
            this.CurrentPhaseVertical = Phase.Attack;
        }

        // These are a set of four, complex release statements for each key. The first is described in detail.
        // This first loop only works if it has been enough time since the other horizontal key was pressed,
        // the player was in attack phase for long enough to speed up to full, and the right key was released.
        if (Input.GetKeyUp(KeyCode.D) && HDualReleaseTimer <= 0 && this.HAttackTimer > this.AttackDuration)
        {
            horizontal = 1.0f;
            this.CurrentPhaseHorizontal = Phase.Release;
        }
        // The second loop doesn't require the the attack phase was finished (as in, the player doesn't have to fully accelerate).
        else if (Input.GetKeyUp(KeyCode.D) && HDualReleaseTimer <= 0)
        {
            // If so, their sliding speed is scaled down, so just tapping the button doesn't result in a huge sliding boost.
            horizontal = this.HAttackTimer / this.AttackDuration;
            this.CurrentPhaseHorizontal = Phase.Release;
        }
        // If the "HDualReleaseTimer," then the other button was recently held. This means the player shouldn't slide at all.
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.CurrentPhaseHorizontal = Phase.None;
            // This switches the current direction if the player let's go of one of the keys after holding both
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1.0f;
                this.CurrentPhaseHorizontal = Phase.Attack;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && HDualReleaseTimer <= 0 && this.HAttackTimer > this.AttackDuration)
        {
            horizontal = -1.0f;
            this.CurrentPhaseHorizontal = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.A) && HDualReleaseTimer <= 0)
        {
            horizontal = -1.0f * this.HAttackTimer / this.AttackDuration;
            this.CurrentPhaseHorizontal = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.CurrentPhaseHorizontal = Phase.None;

            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1.0f;
                this.CurrentPhaseHorizontal = Phase.Attack;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) && VDualReleaseTimer <= 0 && this.VAttackTimer > this.AttackDuration)
        {
            vertical = 1.0f;
            this.CurrentPhaseVertical = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.W) && VDualReleaseTimer <= 0)
        {
            vertical = this.VAttackTimer / this.AttackDuration;
            this.CurrentPhaseVertical = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            this.CurrentPhaseVertical = Phase.None;

            if (Input.GetKey(KeyCode.S))
            {
                vertical = -1.0f;
                this.CurrentPhaseVertical = Phase.Attack;
            }
        }

        if (Input.GetKeyUp(KeyCode.S) && VDualReleaseTimer <= 0 && this.VAttackTimer > this.AttackDuration)
        {
            vertical = -1.0f;
            this.CurrentPhaseVertical = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.S) && VDualReleaseTimer <= 0)
        {
            vertical = -1.0f * this.VAttackTimer / this.AttackDuration;
            this.CurrentPhaseVertical = Phase.Release;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            this.CurrentPhaseVertical = Phase.None;

            if (Input.GetKey(KeyCode.W))
            {
                vertical = 1.0f;
                this.CurrentPhaseVertical = Phase.Attack;
            }
        }


        // These 2 statements scan for no movement. Nested for clarity, as they only set 
        // movement directions to 0 if the player is not currently sliding from the release phase.
        if (this.CurrentPhaseHorizontal == Phase.None)
        {
            if (!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
            {
                horizontal = 0.0f;
            }
        }
        if (this.CurrentPhaseVertical == Phase.None)
        {
            if (!(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.W)))
            {
                vertical = 0.0f;
            }
        }

        // These 2 statements scan for when the player is pressing both keys at once and set the player to 
        // stop and not slide in the release phase (via the DualReleaseTimer system)
        if ((Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.D)))
        {
            horizontal = 0.0f;
            this.ResetTimersHorizontal();
            this.CurrentPhaseHorizontal = Phase.Sustain;
            // Set the dual release timer to a value so that the player doesn't bounce in a random direction after releasing both keys.
            HDualReleaseTimer = DualReleaseThreshold;
        }
        if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.W)))
        {
            vertical = 0.0f;
            this.ResetTimersVertical();
            this.CurrentPhaseVertical = Phase.Sustain;
            VDualReleaseTimer = DualReleaseThreshold;
        }
    }

    // After every throttle period of seconds, the player will check if the current amount of time left is negative and how much.
    // Depending on how much negative time is, the player will suffer temporary debuffs until the player gains enough time.
    // This script is also responsible for playing music for negative time (only if we don't have a second floor).
    private void CheckForNegativeTime()
    {
        currentTime = timer.returnTime();

        if (currentTime <= 0)
        {
            // Unimplemented for now because the blindness effect obliterates the game's frame rate.
            // For now, the player is notified of crunch time with the music change.

            // Apply a screen border effect to notify the player of negative time and also reduce view distance.
        }
        else
        {
            // Unapply that screen border effect
        }

        if (currentTime <= -60.0f && hasRegenDebuff == false)
        {
            hasRegenDebuff = true;
            canRegen = false;
        }
        else if (currentTime > -60.0f && hasRegenDebuff == true)
        {
            hasRegenDebuff = false;
            canRegen = true;
        }

        if (currentTime <= -120.0f)
        {
            ChangeFireRate(0.5f);
            hasFiringDebuff = true;

        }
        else if (currentTime > -120.0f && hasFiringDebuff == true)
        {
            ChangeFireRate(2.0f);
            hasFiringDebuff = false;
        }

        if (currentTime <= -180.0f)
        {
            // If the player reaches this point, they then immediately die.
            currentHealth = 0;
        }


        // NOTE: Remove this if else statement if we add a second floor.
        if (currentTime <= 0.0f && playingNegativeTimeMusic == false)
        {
            soundSystem.PlayMusicTrack("TestGen");
            playingNegativeTimeMusic = true;
        }
        else if (currentTime > 30.0f && playingNegativeTimeMusic)
        {
            // To avoid spastic music changes, there is a threshold to stop playing the negative time music
            // Because the shop stops the timer, this will not break shop music.
            playingNegativeTimeMusic = false;
            // This is the normal music track
            soundSystem.PlayMusicTrack("CrunchTime");
        }

        // Shop music is handeled by other scripts besides being played on gameover.
    }

    // This is a collision script for melee enemies and projectiles. The player takes a constant amount of damage (later it 
    // could be a variable amount stored in an enemyController script). This is called every frame it intersects with the player.
    private void OnTriggerStay2D(Collider2D other)
    {
        // The Enemy tag only exists for backwards compatibility, I recommend using EnemyMelee instead.
        // Every tag corresponds to a different kind of enemy controller script as we transition to multiple enemy types.
        if (other.gameObject.CompareTag("Enemy")) {
            ChangeCurrentHealth( -(other.gameObject.GetComponent<EnemyController>().GetDamage()) );
        }
        if (other.gameObject.CompareTag("EnemyMelee"))
        {
            ChangeCurrentHealth( -(other.gameObject.GetComponent<EnemyController>().GetDamage()) );
        }
        // This is for when the player walks into any tile on the hazards trial map
        if (other.gameObject.CompareTag("Hazard"))
        {
            ChangeCurrentHealth(-damageFromHazards);
        }
        // Ranged enemies don't deal contact damage but shoot many projectiles.
        // IMPORTANT NOTE: Projectiles deal damage in their own collider method so they destroy themselves and
        // are able to deal variable damage to the player. Melee/Ranged enmies also do this.
    }

    // Animation functions; Harrison
    // Flashes when hurt.
    IEnumerator Flash()
    {
        while (InvulnerabilityTimer > 0.0f)
        {
            Color c = sprite.color;
            if (c.a == 0.5f)
            {
                c.a = 1.0f;
            } 
            else
            {
                c.a = 0.5f;
            }
            sprite.color = c;
            yield return new WaitForSeconds(.1f);
        }
        Color b = sprite.color;
        b.a = 1.0f;
        sprite.color = b;
        yield break;
    }
    
    // Faded when invulnerable.
    IEnumerator Faded()
    {
        Color c = sprite.color;
        c.a = 0.5f;
        sprite.color = c;
        while (InvulnerabilityTimer > 0.0f)
        {
            yield return new WaitForSeconds(dashTimeLength);
        }
        Color b = sprite.color;
        b.a = 1.0f;
        sprite.color = b;
        yield break;
    }
    
    // Beginning of public interface functions:

    // This increases (with a positive argument) or decreases (with a negative argument) the player's current health.
    // If the player is invulnerable (recently damaged or dashing), they can't be damaged through this.
    public void ChangeCurrentHealth(float hitPointsToAdd)
    {
        if ( (InvulnerabilityTimer > 0) && (hitPointsToAdd < 0) )
        {
            return;
        }
        else
        {
            currentHealth += hitPointsToAdd;

            // Set vulnerability timer if damaged, or ensure max health is respected if healed.
            if (hitPointsToAdd < 0)
            {
                InvulnerabilityTimer = invulnerabilityTime;
                Debug.Log("Took Damage: " + hitPointsToAdd);
                soundSystem.PlaySoundEffect("PlayerHit");
                animator.SetTrigger("Hurt");
                StartCoroutine(Flash());
            }
            else if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Game-Over here");
            // The gameover music is shared with title and shop music.
            soundSystem.PlayMusicTrack("Altar");
        }
    }

    // Intended for HP upgrades (though also functional with downgrades), give the player a new max health and a full heal.
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        // Give the player a full heal to go with the maxHealth change
        currentHealth = newMaxHealth;
    }
    // Intended for speed upgrades/downgrades, give the player a new speed.
    public void SetSpeed(float newSpeed)
    {
        if (isDashing)
        {
            normalSpeed = newSpeed;
        }
        else
        {
            speed = newSpeed;
            normalSpeed = newSpeed;
        }
    }
    // Intended for dashing upgrades/downgrades, give the player a new time spent dashing.
    public void SetDashTime(float newDashTime)
    {
        dashTimeLength = newDashTime;
    }
    // Intended for dashing upgrades/downgrades, give the player a new speed when dashing.
    public void SetDashSpeed(float newDashSpeed)
    {
        dashSpeed = newDashSpeed;
    }
    // Intended for dashing upgrades/downgrades, give the player a new dash cooldown.
    // The length of the cooldown is dashCooldown * 3 seconds.
    public void SetDashCooldown(float newDashCooldown)
    {
        dashCooldown = newDashCooldown;
    }
    // Intended for firing upgrades/downgrades, multiply the angle of inaccuracy when firing.
    // This is recommended along with a heavy firing speed upgrade to make it a little more interesting.
    // If a multishot upgrade is implemented, this will go along well with it too.
    public void ChangeInaccuracy(float newInaccuracyModifier)
    {
        inaccuracy *= newInaccuracyModifier;
    }
    // Intended for projectile upgrades/downgrades, multiply current damage by a specific multiplier.
    public void ChangeDamage(float newDamageMultiplier)
    {
        damage *= newDamageMultiplier;
    }
    // Intended for firing upgrades/downgrades, multiply the speed of the current fire rate via division. Input a number
    //  less than one for slower firing, or a number higher than one for that many times more projectiles per second.
    public void ChangeFireRate(float newFireRateMultiplier)
    {
        if (newFireRateMultiplier <= 0)
        {
            Debug.Log("no");
        }
        else
        {
            // The fire rate is divided because lower fire rates are faster.
            FireRate = FireRate / newFireRateMultiplier;
        }
    }
    // Intended for projectile upgrades/downgrades, multiply current damage by a specific multiplier.
    public void ChangeRegen(float newRegenMultiplier)
    {
        regenTimerRate *= newRegenMultiplier;
    }

    // The two functions below are simple getter functions for current and max health respectively.
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetDashCooldown()
    {
        return dashCooldown;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

}