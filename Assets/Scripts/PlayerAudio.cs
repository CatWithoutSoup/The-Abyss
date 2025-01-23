//using UnityEngine;

//public class PlayerAudio : MonoBehaviour
//{
//    public AudioClip walkSound;  // Звук ходьбы
//    public AudioClip climbSound; // Звук карабканья
//    private PlayerMovement player;
//    private AudioSource audioSource;  // Ссылка на компонент AudioSource

//    void Start()
//    {
//        player = GetComponent<PlayerMovement>();
//        audioSource = GetComponent<AudioSource>();
//    }

//    void Update()
//    {
//        if (player.rb.velocity.x != 0 && !audioSource.isPlaying)
//        {
//            PlaySound(walkSound);
//        }

//        // Проверка на карабкание
//        if (Input.GetAxisRaw("Vertical") != 0 && player.wallGrab && !audioSource.isPlaying)
//        {
//            PlaySound(climbSound);
//        }
//    }

//    // Метод для воспроизведения звука
//    void PlaySound(AudioClip clip)
//    {
//        audioSource.clip = clip;  // Устанавливаем нужный клип
//        audioSource.Play();       // Воспроизводим звук
//    }

//}