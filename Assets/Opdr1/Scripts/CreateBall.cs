using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Opdr1.Scripts
{
    public class CreateBall : MonoBehaviour
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        public GameObject ballPrefab;
        private float elapsedTime = 0f;


        private void Start()
        {
            for (var i = 0; i < 100; i++)
            {
                var color = GenerateRandomColor();
                var randPos = GenerateRandomPosition();
                CreateBal(color, randPos);
            }
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            if (!(elapsedTime > 1f)) return;
            CreateAndDestroyBall();
            elapsedTime = 0f;
        }

        private void CreateAndDestroyBall()
        {
            var randColor = GenerateRandomColor();
            var randPosition = GenerateRandomPosition();
            var ball = CreateBal(randColor, randPosition);
            DestroyBall(ball);
        }

        private Color GenerateRandomColor()
        {
            var r = Random.Range(0f, 1f);
            var g = Random.Range(0f, 1f);
            var b = Random.Range(0f, 1f);
            return new Color(r, g, b, 1f);
        }

        private Vector3 GenerateRandomPosition()
        {
            return new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 10f),
                Random.Range(-10f, 10f)
            );
        }

        private GameObject CreateBal(Color c, Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            Material material = ball.GetComponent<MeshRenderer>().material;
            if (material.shader.name == "Universal Render Pipeline/Lit")
            {
                material.SetColor(BaseColor, c);
            }
            DestroyBall(ball);
            return ball;
        }

        private void DestroyBall(GameObject ball)
        {
            Destroy(ball, 3f);
        }
    }
}