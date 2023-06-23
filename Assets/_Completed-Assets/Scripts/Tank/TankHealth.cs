using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Complete
{
    public class TankHealth : MonoBehaviour
    {
        public BoostVariables boost;
        

        public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
        public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        public Image m_FillImage;                           // The image component of the slider.
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
        
        
        private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?


        public float boostScale;


        public void Start ()
        {
            boost.boostCurrent = boost.boostTime;


            boostScale = m_StartingHealth / boost.boostTime;
            Debug.Log("boostScale = " + boostScale);

            boost.speedCurrent = boost.speed;
        }

        public void Update()
        {
            // Trigger boost
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (boost.isCooldown == false)
                {
                    StartCoroutine(BoostCoroutine());
                }
            }

            
        }


        private void Awake ()
        {
            

            // Instantiate the explosion prefab and get a reference to the particle system on it.
            m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

            // Get a reference to the audio source on the instantiated prefab.
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

            // Disable the prefab so it can be activated when it's required.
            m_ExplosionParticles.gameObject.SetActive (false);

            boost.boostCurrent = boost.boostTime;

            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, (boost.boostCurrent * boostScale) / m_StartingHealth);
            m_FillImage.fillAmount = Mathf.Clamp01(boost.boostCurrent * boostScale / m_StartingHealth);

            boost.isBoosting = false;
            boost.isCooldown = false;

        }


        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;



            // Update the health slider's value and color.
            SetHealthUI();
        }


        private IEnumerator BoostCoroutine()
        {
            // Check if user is mid-boost or in cooldown
            if (boost.isBoosting == false && boost.isCooldown == false)
            {
                // user is boosting
                boost.isBoosting = true;
                boost.isCooldown = true;

                boost.speedCurrent = boost.boostSpeed;


                // While user is boosting
                while (boost.boostCurrent > 0.05f)
                {

                    boost.boostCurrent -= Time.deltaTime;

                    // Change UI elements
                    Debug.Log("boost.boostCurrent = " + boost.boostCurrent);

                    m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, (boost.boostCurrent * boostScale) / m_StartingHealth);

                    m_FillImage.fillAmount = Mathf.Clamp01(boost.boostCurrent * boostScale / m_StartingHealth);

                    yield return null;
                }
            }



            // catch it should be 0
            boost.boostCurrent = 0;

            // stop boosting
            boost.isBoosting = false;

            // set speed back to normal
            boost.speedCurrent = boost.speed;

            // Cooldown and refil
            StartCoroutine(boostCooldown());

        }




        //
        //
        //
        // 
        public void TakeDamage (float amount)
        {
            /*
            // While user is boosting
            while (boost.boostCurrent != 0)
            {
                boost.boostCurrent -= Time.deltaTime;

                // Change UI elements
                SetHealthUI();
            }

            // catch it should be 0
            boost.boostCurrent = 0;

            // Reduce current health by the amount of damage done.
            //m_CurrentHealth -= amount;

            // Change the UI elements appropriately.
            //SetHealthUI ();
            /*
            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath ();
            }
            */

            // Cooldown and refil
            //StartCoroutine(boostCooldown());
            
            
        }

        private IEnumerator boostCooldown()
        {
            yield return new WaitForSeconds(boost.cooldown);

            boost.boostCurrent = boost.boostTime;

            Debug.Log("Cooldown over");

            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, (boost.boostCurrent * boostScale) / m_StartingHealth);
            m_FillImage.fillAmount = Mathf.Clamp01(boost.boostCurrent * boostScale / m_StartingHealth);

            // end cooldown
            boost.isCooldown = false;
        }





        private void SetHealthUI ()
        {
            boostScale = m_StartingHealth / boost.boostTime;

            // Set the slider's value appropriately.
            // use boostScale to keep with scale of 100
            m_Slider.value = 100;
            

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, (boost.boostCurrent * boostScale) / m_StartingHealth);
        }


        private void OnDeath ()
        {
            // Set the flag so that this function is only called once.
            m_Dead = true;

            // Move the instantiated explosion prefab to the tank's position and turn it on.
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive (true);

            // Play the particle system of the tank exploding.
            m_ExplosionParticles.Play ();

            // Play the tank explosion sound effect.
            m_ExplosionAudio.Play();

            // Turn the tank off.
            gameObject.SetActive (false);
        }
    }
}