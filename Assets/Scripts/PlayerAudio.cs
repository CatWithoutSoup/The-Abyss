//using UnityEngine;

//public class PlayerAudio : MonoBehaviour
//{
//    public AudioClip walkSound;  // ���� ������
//    public AudioClip climbSound; // ���� ����������
//    private PlayerMovement player;
//    private AudioSource audioSource;  // ������ �� ��������� AudioSource

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

//        // �������� �� ����������
//        if (Input.GetAxisRaw("Vertical") != 0 && player.wallGrab && !audioSource.isPlaying)
//        {
//            PlaySound(climbSound);
//        }
//    }

//    // ����� ��� ��������������� �����
//    void PlaySound(AudioClip clip)
//    {
//        audioSource.clip = clip;  // ������������� ������ ����
//        audioSource.Play();       // ������������� ����
//    }

//}