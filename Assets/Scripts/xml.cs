using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class xml : MonoBehaviour
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
        string filePath = Path.Combine(Application.streamingAssetsPath, "user.xml");
        List<User> lista = new List<User>();

        User user = new User();
        user.login = nome.text;
        user.senha = senha.text;

        //tratar/validar usuarios

        print("Login: " + user.login);
        print("Senha: " + user.senha);

        lista.Add(user);

       

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if (!File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer.BaseStream, lista);
            writer.Close();
        }
        else
        {
            XmlSerializer serializerVerificacao = new XmlSerializer(typeof(List<User>));
            StreamReader leitor = new StreamReader(filePath);
            List<User> usuariosAtuais = (List<User>)serializerVerificacao.Deserialize(leitor.BaseStream);
            leitor.Close();

            foreach (User item in usuariosAtuais)
            {
                if (user.login == item.login)
                {
                    print("Usuário " + user.login + " já existe");
                    return;
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            StreamReader reader = new StreamReader(filePath);
            List<User> deserialized = (List<User>)serializer.Deserialize(reader.BaseStream);
            reader.Close();

            deserialized.Add(user);
            //deserialized.AddRange(lista);

            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer.BaseStream, deserialized);
            writer.Close();
        }
    }

    public void ler()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "user.xml");

        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        StreamReader reader = new StreamReader(filePath);
        List<User> deserialized = (List<User>)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        foreach (User item in deserialized)
        {
            print(item.login);
        }
    }

    public void apagar()
    {
        File.Delete(Application.streamingAssetsPath + "/user.xml");
    }
}
