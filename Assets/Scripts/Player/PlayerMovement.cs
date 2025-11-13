using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    private float playerSpeed = 10f;
    private Rigidbody2D playerRigidBody;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Transform playerPosition;
    private Vector2 networkPosition;

    private bool isNetworked = false;

    // 🔹 Novos campos para interação
    private bool isNearInteractable = false;
    private GameObject currentInteractable;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        isNetworked = photonView != null && photonView.ViewID != 0;

        if (!isNetworked || photonView.IsMine)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName != "gameTrello" && PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");

                playerPosition.position = new Vector2(x, y);
            }
        }
    }

    void Update()
    {
        // O player pode se mover apenas localmente
        if (isNetworked && !photonView.IsMine) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = playerSpeed * moveInput;

        // 🔹 Interação com objetos ao pressionar "E"
        if (isNearInteractable && Input.GetKeyDown(KeyCode.E))
        {
            InteragirComObjeto();
        }
    }

    private void FixedUpdate()
    {
        if (playerRigidBody == null) return;

        if (!isNetworked || photonView.IsMine)
        {
            playerRigidBody.linearVelocity = moveVelocity;
        }
        else
        {
            // Interpolação suave dos outros jogadores
            playerRigidBody.position = Vector2.Lerp(playerRigidBody.position, networkPosition, Time.fixedDeltaTime * 10);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!isNetworked) return;

        if (stream.IsWriting)
        {
            // Envia a posição do player para o servidor
            stream.SendNext(playerRigidBody.position);
        }
        else
        {
            // Recebe a posição dos outros jogadores
            networkPosition = (Vector2)stream.ReceiveNext();
        }
    }

    // 🔹 Métodos para interação
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            isNearInteractable = true;
            currentInteractable = other.gameObject;
            Debug.Log("Player entrou na área de interação com " + other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            isNearInteractable = false;
            currentInteractable = null;
            Debug.Log("Player saiu da área de interação");
        }
    }

    private void InteragirComObjeto()
    {
        if (currentInteractable != null)
        {
            Debug.Log("Interagindo com " + currentInteractable.name);

            // Exemplo: ativar um Canvas ou abrir uma tela
            var canvas = currentInteractable.GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                bool ativo = canvas.gameObject.activeSelf;
                canvas.gameObject.SetActive(!ativo);

                // Pausar o jogo enquanto inspeciona
                Time.timeScale = ativo ? 1f : 0f;
            }
        }
    }
}
