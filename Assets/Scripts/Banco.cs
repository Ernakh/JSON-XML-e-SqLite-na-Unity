using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.SqliteClient;

public class Banco : MonoBehaviour
{
    /*
    
        Fazer um jogo onde seja armazenado o inventário do personagem.
        A cada 30 segundos, itens aparecem espalhados pelo cenáro.
        Quando o personagem pega um item, este é adicionado ao inventário.
        Se o item já existe no inventário, a quantidade do item deve ser alterada, ou seja, não é feito uma nova inserção, mas um update.
        O jogador deve inserir o nome antes de iniciar o jogo.
        Quando o jogador insere um nome já existente, o inventário é carregado.
        Configurar uma tecla para mostrar os itens do inventário.

    */

    private IDbConnection conec;
    private IDbCommand command;
    private IDataReader reader;

    private string stringConexao = "URI=File:meuBanco.db";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool conectar()
    {
        try
        {
            conec = new SqliteConnection(stringConexao);
            command = conec.CreateCommand();
            conec.Open();

            string comandoSql = "CREATE TABLE IF NOT EXISTS USUARIOS(ID INTEGER PRIMARY KEY AUTOINCREMENT, NOME VARCHAR(50));";

            command.CommandText = comandoSql;
            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }

    public bool inserir(string nome)
    {
        try
        {
            conectar();

            //string comandoSql = "INSERT INTO USUARIOS(NOME) VALUES ($nome);";
            string comandoSql = "INSERT INTO USUARIOS(NOME) VALUES ('" + nome + "');";

            command.CommandText = comandoSql;
            //command.Parameters.Add(nome);
            command.ExecuteNonQuery();
            
            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }

    public bool consultar()
    {
        try
        {
            conectar();

            string comandoSql = "SELECT * FROM USUARIOS;";

            command.CommandText = comandoSql;
            reader = command.ExecuteReader();

            int cont = 0;

            while (reader.Read())
            {
                print("ID: " + reader.GetInt32(0));
                print("NOME: " + reader.GetString(1));

                cont++;
            }

            print("Total de Registros: " + cont);

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }

    public bool alterar(int id, string nome)
    {
        try
        {
            conectar();

            string comandoSql = "UPDATE USUARIOS SET NOME = '" + nome + "' WHERE ID = " + id + ";";
            //string comandoSql = "UPDATE USUARIOS SET NOME = $nome WHERE ID = $id;";

            command.CommandText = comandoSql;

            //command.Parameters.Add(nome);
            //command.Parameters.Add(id);

            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }

    public bool remover(int id)
    {
        try
        {
            conectar();

            string comandoSql = "DELETE FROM USUARIOS WHERE ID = " + id + ";";
            //string comandoSql = "DELETE FROM USUARIOS WHERE ID = $id;";

            command.CommandText = comandoSql;

            //command.Parameters.Add(id);

            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
}
