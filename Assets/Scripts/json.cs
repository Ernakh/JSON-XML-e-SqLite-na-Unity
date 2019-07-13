using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class json : MonoBehaviour
{
    public Text nome;
    public Text senha;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gravar()
    {
        User user = new User();
        user.login = nome.text;
        user.senha = senha.text;

        print("Login: " + user.login);
        print("Senha: " + user.senha);

        string json = "";

        string filePath = Path.Combine(Application.streamingAssetsPath, 
            user.login + ".json");

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if (!File.Exists(filePath))
        {
            //List<User> lista = new List<User>();
            //json = JsonUtility.ToJson(lista);

            json = JsonUtility.ToJson(user);
            //MemoryStream stream2 = new MemoryStream();
            //DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(User));
            ////DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            //ds.WriteObject(stream2, user);
            //json = Encoding.UTF8.GetString(stream2.ToArray());
            //stream2.Close();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(json);
            }
        }
        else
        {
            print("Usuário já existe");
        }
    }

    public void ler()
    {
        try
        {
            //using (Stream stream = File.OpenRead(
            //    Application.streamingAssetsPath + "/" + nome.text + ".json"))
            //{
            //    var serializer = new DataContractJsonSerializer(typeof(User));
            //    User user = (User)serializer.ReadObject(stream);

                string json = System.IO.File.ReadAllText(
                    Application.streamingAssetsPath + "/" + nome.text + ".json");

                User user = JsonUtility.FromJson<User>(json);

                if (user.senha == senha.text)
                {
                    print("Bem Vindo!");
                }
                else
                {
                    print("Senha não confere!");
                }
            //}
        }
        catch (Exception ex)
        {
            print("Não encontrado");
        }
    }

    public void lerDiretorio()
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach (FileInfo file in di.GetFiles())
        {
            print(file.Name);
        }
    }

    public void apagar()
    {
        File.Delete(Application.streamingAssetsPath + "/" + nome.text + ".json");
    }
}
