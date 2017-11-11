using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace MD5
{
    class EncriptarMD5
    {
        public string Key { get; set; }
        //public string Key
        //{
        //    get
        //    {
        //        return "DRH";
        //    }

        //    set
        //    {
        //        key = "DRH";
        //    }
        //}

        public string EncriptarMD5_1(string text)
        {
            Key = "DRH";
            try
            {

                byte[] keyarray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(text);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyarray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyarray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();

                text = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);



            }
            catch (Exception)
            {

            }
            return text;
        }

        public string DesencriptarMD5(string textoEncriptado)
        {
            try
            {

                byte[] keyArray;
                byte[] Array_a_Descriptar = Convert.FromBase64String(textoEncriptado);
                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransfrom = tdes.CreateDecryptor();
                byte[] ResultArray = cTransfrom.TransformFinalBlock(Array_a_Descriptar, 0, Array_a_Descriptar.Length);
                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(ResultArray);



            }
            catch (Exception)
            {

            }
            return textoEncriptado;

        }

    }
}
