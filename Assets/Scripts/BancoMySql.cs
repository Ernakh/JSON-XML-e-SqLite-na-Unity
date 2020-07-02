using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

public class BancoMySql
{
    public MySqlConnection conexao(string conec)
        {
            MySqlConnection cn = new MySqlConnection(conec);
            return cn;
        }

        public MySqlConnection abrirConexao()
        {
            string conec = "Server=gamesunity.mysql.dbaas.com.br;Database=gamesunity;User Id=gamesunity;Password=ufn_unity;";
            MySqlConnection cn = conexao(conec);

            try
            {
                cn.Open();
                return cn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void fecharConexao(MySqlConnection cn)
        {
            try
            {
                cn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet executeQuery(MySqlCommand sql)
        {
            try
            {
                sql.Connection = abrirConexao();
                sql.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(sql);

                DataSet ds = new DataSet();
                da.Fill(ds);

                fecharConexao(sql.Connection);

                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

            }
        }

        internal void excluir(int id)
        {
            MySqlTransaction tran = null;
            MySqlConnection cn = abrirConexao();
            MySqlCommand command = new MySqlCommand();

            tran = cn.BeginTransaction();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "delete from usuarios where id = @id";

            command.Parameters.Add("@id", MySqlDbType.Int32);
            command.Parameters[0].Value = id;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                fecharConexao(cn);
            }
        }

        internal bool inserir(string email, string senha)
        {
            MySqlTransaction tran = null;
            MySqlConnection cn = abrirConexao();
            MySqlCommand command = new MySqlCommand();

            tran = cn.BeginTransaction();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into usuarios(email, senha) values(@email, @senha)";
           
            command.Parameters.AddWithValue("@email", email);
			command.Parameters.AddWithValue("@senha", senha);

            command.Parameters[0].Value = email;
            command.Parameters[1].Value = senha;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
				return true;
            }
            catch (Exception e)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                fecharConexao(cn);
            }
        }

        public DataTable executeQuery(string sql)
        {
            MySqlConnection cn = null;

            try
            {
                MySqlCommand sqlComm = new MySqlCommand(sql, cn = abrirConexao());

                sqlComm.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(sqlComm);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                fecharConexao(cn);
            }
        }

        internal int consultar(string email, string senha)
        {
            MySqlConnection cn = null;

            try
            {
                MySqlCommand x = new MySqlCommand();
                x.CommandText = "select * from USUARIOS where EMAIL = @login and SENHA = @senha";
                x.Parameters.Add("@login", MySqlDbType.VarChar);
                x.Parameters.Add("@senha", MySqlDbType.VarChar);
                x.Parameters[0].Value = email;
                x.Parameters[1].Value = senha;

                cn = abrirConexao();
                x.Connection = cn;
                MySqlDataAdapter da = new MySqlDataAdapter(x);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                fecharConexao(cn);
            }
        }
}
