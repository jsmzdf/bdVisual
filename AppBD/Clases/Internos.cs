using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace AppBD.Clases
{
    class Internos
    {
        public Conexion con = new Conexion();
        public DataSet TABLA;
        public OleDbDataAdapter ORDEN;

        DataTable dt = new DataTable();

        public void consultarI(string consulta)
        {

            con.conectar();
            ORDEN = new OleDbDataAdapter("SELECT INTERNOS.ITEM,FECHA_DE_CORREO," +
                "FECHA_RECIBIDO,FUNCIONARIO_ENTREGA,MES_RECIBO,DOCUMENTOS_RECIBIDOS," +
                "INTERNOS.NO_FOLIOS,INTERNOS.OBSERVACIONES" +
                " FROM [CONTRATO]" +
                 " INNER JOIN ([INTERNOS_CONTRATOS]  INNER JOIN [INTERNOS]" +
                 //    relacionid                       radicadoitem
                 " ON [INTERNOS_CONTRATOS].ITEM=[INTERNOS].ITEM) " +
                 //    
                 " ON [CONTRATO].CONTRATO_NO=[INTERNOS_CONTRATOS].[CONTRATO_AÑO] " +
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
