using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonControllerEditMe : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] public float m_WalkSpeed;
        [SerializeField] public float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private bool hit;
        private Animator ani;
        private AudioSource m_AudioSource;
        private DamageFlash damageFlash;
        private HealFlash healFlash;
        public AudioClip damageSound;
        public AudioClip attackSound;
        public AudioClip healSound;
        private float hittimer;
        public int lives;
        public spawnpoint respawn;
        private WeaponStats weapon;
        private bool attackSoundPlay;
        float attackSoundCooldown;
        public Slider Healthbar;

        public static float m_walkSpeed
        {
            get { return m_walkSpeed; }
            set { m_walkSpeed = value; }

        }
        // Use this for initialization
        private void Start()
        {
            hittimer = 0;
            lives = game_settings.playerLives-1;
            m_CharacterController = GetComponent<CharacterController>();
            ani = GetComponentInChildren<ResetPostion>().GetComponent<Animator>();

            m_Camera = GetComponentInChildren<Camera>();
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            weapon = GetComponentInChildren<WeaponStats>();
            damageFlash = (DamageFlash)GetComponent(typeof(DamageFlash));
            healFlash = (HealFlash)GetComponent(typeof(HealFlash));
            attackSoundPlay = true;

            //Healthbar = GetComponentInChildren<Slider>();
        }


        // Update is called once per frame
        private void Update()
        {
           if (GameStateManager.GetCurrentState()!=GameStateManager.Types.Setup)
            {
                ani.SetBool("Attacking", false);
                ani.SetBool("Hit", false);
                if (Healthbar.value > 0)
                {
                    //if (Healthbar.value <= 0)
                    //{
                    //send to game over screen?
                    //    return;
                    //}

                    RotateView();
                    // the jump state needs to read here to make sure it is not missed
                    if (!m_Jump)
                    {
                        m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                    }

                    if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                    {
                        StartCoroutine(m_JumpBob.DoBobCycle());
                        PlayLandingSound();
                        m_MoveDir.y = 0f;
                        m_Jumping = false;
                    }
                    if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                    {
                        m_MoveDir.y = 0f;
                    }

                    //Remove!
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        TakeDamage(5);
                    }
                    //Remove!

                    m_PreviouslyGrounded = m_CharacterController.isGrounded;
                }
                else if(lives>0)
                {
                    lives += -1;
                    Healthbar.value = 100;
                    this.transform.position = respawn.transform.position;
                }
                else
                {
                    ani.SetBool("Dead", true);
                    GameStateManager.SetCurrentState(GameStateManager.Types.ViveWin);
                    if (ani.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Death1"))
                    {
                        ani.StopPlayback();
                    }
                }

                if (weapon.WeaponAnimations.GetBool("isAttacking") && attackSoundPlay)
                {
                    attackSoundPlay = false;
                    attackSoundCooldown = Time.time;
                    AudioSource.PlayClipAtPoint(attackSound, transform.position, 0.3f);
                    ani.SetBool("Attacking", true);

                }

                if (Time.time >= attackSoundCooldown + 1.0f)
                {
                    attackSoundPlay = true;
                } 
            }
        }

        public void alterDamage(int amount)
        {
            weapon.DamageValue += amount;
        }

        public void thisIsDumb()
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.distance < weapon.WeaponRange)
                {
                    hit.transform.SendMessage("ApplyDamage", weapon.DamageValue, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        public void FireRangedAttack()
        {
            var clone = Instantiate(weapon.Projectile, weapon.transform.position, weapon.transform.rotation);
            clone.velocity = this.transform.TransformDirection(new Vector3(0, 0, weapon.ProjectileSpeed));

            Destroy(clone.gameObject, 3);
        }

        public void TakeDamage(int DamageToTake)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position, 0.3f);
            damageFlash.trigger = true;
            Healthbar.value -= DamageToTake;
            ani.SetBool("Hit", true);
        }

        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        public void EditHealthBar(int amount)
        {
            healFlash.healTrigger = true;
            Healthbar.value += amount;
            AudioSource.PlayClipAtPoint(healSound, transform.position);
        }

        private void FixedUpdate()
        {
            if (GameStateManager.GetCurrentState() != GameStateManager.Types.Setup)
            {
                if (Healthbar.value > 0)
                {
                    float speed;
                    GetInput(out speed);
                    // always move along the camera forward as it is the direction that it being aimed at
                    Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

                    // get a normal for the surface that is being touched to move along it
                    RaycastHit hitInfo;
                    Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                       m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                    desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                    m_MoveDir.x = desiredMove.x * speed;
                    m_MoveDir.z = desiredMove.z * speed;

                    if (m_CharacterController.isGrounded)
                    {
                        m_MoveDir.y = -m_StickToGroundForce;

                        if (m_Jump)
                        {
                            m_MoveDir.y = m_JumpSpeed;
                            PlayJumpSound();
                            m_Jump = false;
                            m_Jumping = true;
                        }
                    }
                    else
                    {
                        m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
                    }
                    m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

                    ProgressStepCycle(speed);
                    UpdateCameraPosition(speed);

                    m_MouseLook.UpdateCursorLock();
                }
            }

        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
