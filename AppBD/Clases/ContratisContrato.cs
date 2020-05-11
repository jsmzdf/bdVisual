using System.Data;
using System.Data.OleDb;
namespace AppBD.Clases
{
    class ContratisContrato
    {
        public Conexion con = new Conexion();
        DataTable dt = new DataTable();
        OleDbCommand ORDENU;

        public void addConContri(double idCotratista, string con_no)
        {
            con.conectar();
            string q = "INSERT INTO [CONTRATISTA_CONTRATO](ID_CONTRATISTA,CONTRATO_NO) " +
               "values(@ID_CONTRATISTA,@CONTRATO_NO)";
            ORDENU = new OleDbCommand(q, con.CANAL);
            ORDENU.Parameters.Add(new OleDbParameter("@ID_CONTRATISTA", OleDbType.Double));
            ORDENU.Parameters["@ID_CONTRATISTA"].Value = idCotratista;
            ORDENU.Parameters.Add(new OleDbParameter("@CONTRATO_NO", OleDbType.VarChar));
            ORDENU.Parameters["@CONTRATO_NO"].Value = con_no;

            ORDENU.Connection.Open();
            ORDENU.ExecuteNonQuery();
            ORDENU.Connection.Close();
        }
    }
}
