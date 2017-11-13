using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DrHuellitas.BO
{
    public class FotoBO
    {
        byte[] arr;
        public byte[] ConvertirAFoto(HttpPostedFileBase file)
        {

            if (file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    arr = ms.GetBuffer();

                }
            }
            return arr;
        }
    }
}