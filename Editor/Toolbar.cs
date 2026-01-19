using UnityEngine;
using UnityEditor.Toolbars;
using UnityEditor.Overlays;
using UnityEditor;

namespace MonumentGames.PlayerMovement3D
{
    [EditorToolbarElement(id, typeof(SceneView))]
    class CreatePlayerButton : EditorToolbarButton
    {
        public const string id = "PlayerMovement3D/CreatePlayer";
        public static GameObject cam;

        public CreatePlayerButton()
        {
            text = "Create Player";
            icon = (Texture2D)EditorGUIUtility.IconContent("AvatarSelector").image;
            tooltip = "Instantiates the player with all components and default settings in the scene";
            clicked += OnClick;
        }

        void OnClick()
        {
            // Create player capsule
            Transform newObj = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform;
            Object.DestroyImmediate(newObj.GetComponent<MeshRenderer>());

            // Add default options to player
            newObj.name = "Player";
            newObj.gameObject.AddComponent<PlayerMovement>();
            newObj.gameObject.AddComponent<Rigidbody>().freezeRotation = true;
            Undo.RegisterCreatedObjectUndo(newObj.gameObject, "Created Player");

            // Create player camera
            Transform newCam = new GameObject("Camera").transform;
            newCam.gameObject.AddComponent<Camera>();

            // Add default options to camera
            newCam.SetParent(newObj);
            newCam.position = new(0f, 0.5f, 0f);
            newObj.gameObject.GetComponent<PlayerMovement>().cam = newCam.gameObject.GetComponent<Camera>();
            Undo.RegisterCreatedObjectUndo(newCam.gameObject, "Created Camera");
        }

        void CreateInventory()
        {

        }
    }

    [Overlay(typeof(SceneView), "Player Movement")]
    [Icon("Assets/unity.png")]
    public class PlayerMovementToolbar : ToolbarOverlay
    {
        PlayerMovementToolbar() : base(
            CreatePlayerButton.id
        )
        {
        }
    }
}