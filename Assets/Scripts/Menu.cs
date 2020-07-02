using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	//sqlite
    public Text id;
    public Text nome;
	
	//mysql
	public Text email;
    public Text senha;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cadastrar()
    {
        Banco bd = new Banco();
        if(bd.inserir(nome.text))
        {
            print("Salvo com sucesso!");
        }
        else
        {
            print("Erro ao salvar!");
        }
    }

    public void Alterar()
    {
        Banco bd = new Banco();
        if (bd.alterar(int.Parse(id.text), nome.text))
        {
            print("Alterado com sucesso!");
        }
        else
        {
            print("Erro ao alterar!");
        }
    }

    public void Remover()
    {
        Banco bd = new Banco();
        if (bd.remover(int.Parse(id.text)))
        {
            print("Removido com sucesso!");
        }
        else
        {
            print("Erro ao remover!");
        }
    }

    public void Consultar()
    {
        Banco bd = new Banco();
        if (bd.consultar())
        {
            print("Consultado com sucesso!");
        }
        else
        {
            print("Erro ao consultar!");
        }
    }
	
	 public void CadastrarMySql()
    {
        BancoMySql bd = new BancoMySql();
        if(bd.inserir(email.text, senha.text))
        {
            print("Salvo com sucesso!");
        }
        else
        {
            print("Erro ao salvar!");
        }
    }

    /*public void AlterarMySql()
    {
        BancoMySql bd = new BancoMySql();
        if (bd.alterar(int.Parse(id.text), nome.text))
        {
            print("Alterado com sucesso!");
        }
        else
        {
            print("Erro ao alterar!");
        }
    }*/

    /*public void RemoverMySql()
    {
        BancoMySql bd = new BancoMySql();
        if (bd.excluir(int.Parse(id.text)))
        {
            print("Removido com sucesso!");
        }
        else
        {
            print("Erro ao remover!");
        }
    }*/

    public void ConsultarMySql()
    {
        BancoMySql bd = new BancoMySql();
        if (bd.consultar(email.text, senha.text) != 0)
        {
            print("Consultado com sucesso!");
        }
        else
        {
            print("Erro ao consultar!");
        }
    }
}
