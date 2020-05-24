using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace AppBD.Clases
{
    class Radicados
    {
        public Conexion con = new Conexion();
        public DataSet TABLA;
        public OleDbDataAdapter ORDEN;

        DataTable dt = new DataTable();
     
        public void consultarR(string consulta)
        {

            con.conectar();
            ORDEN = new OleDbDataAdapter("SELECT RADICADOS.ITEM," +
                "[FECHA DE RADICACION], [FECHA RECIBIDO],[MES RECIBO]," +
                "RADICADO,[CONTRATO NO],RADICADOS.[AÑO]," +
                "[DOCUMENTOS RECIBIDOS],[NO FOLIOS],RADICADOS.OBSERVACIONES," +
                "[FUNCIONARIO SIGA], REVISION" +
                " FROM [CONTRATO]" +
                 " INNER JOIN ([RADICADOS_CONTRATO]  INNER JOIN [RADICADOS]" +
                 //    relacionid                       radicadoitem
                 " ON [RADICADOS_CONTRATO].ITEM=[RADICADOS].ITEM) " +
                 //    
                 " ON [CONTRATO].CONTRATO_NO=[RADICADOS_CONTRATO].[CONTRATO_AÑO] " +
                 "WHERE [CONTRATO].CONTRATO_NO =@CLABUSCAR", con.CANAL);
            ORDEN.SelectCommand.Parameters.Add(new OleDbParameter("@CLABUSCAR", OleDbType.VarWChar));
            ORDEN.SelectCommand.Parameters["@CLABUSCAR"].Value = consulta;
            dt = new DataTable();
            TABLA = new DataSet();
            ORDEN.Fill(TABLA);
            dt = TABLA.Tables[0];
        }
    }
}
