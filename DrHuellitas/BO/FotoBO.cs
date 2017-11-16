using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

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

        public static Bitmap  RedimensionarImagen(Image imagenOriginal, int width, int height)
        {
            //Obtener el tamaño maximo
            var Radio = Math.Max((double)width / imagenOriginal.Width, (double)height / imagenOriginal.Height);
            //Nuevo ancho y alto según el Radio redimensionado
            var NuevoAncho = (int)(imagenOriginal.Width * Radio);
            var NuevoAlto = (int)(imagenOriginal.Height * Radio);
            //Creamos el bitmap con los nuevos tamaños
            var ImagenRedomencionada = new Bitmap(NuevoAncho,NuevoAlto);
            //copiar la imagen y la convertimos en bitmap
            Graphics.FromImage(ImagenRedomencionada).DrawImage(imagenOriginal, 0, 0, NuevoAncho, NuevoAlto);
            Bitmap ImagenFinal = new Bitmap(ImagenRedomencionada);
            return ImagenFinal;
           
             
        }

        /*
          boton redimensionar
             if(Redimencionar)
             {
                 //informacion de los archivos
                string[] files = Directory.GetFiles(txtRutaOrigen.Text);
                String NombreArchivo;
                String NuevaCarpetaArchivo;
                NuevaCarpetaArchivo = txtRutaDestino.Text + "\\";
                //Sacamos los datos del arreglo de archivos(en este caso es jpg)
                for(int i = 0; i < files.Length; i++)
                {
                    if(Path.GetExtension(files[i])== ".jpg")
                    {
                        NombreArchivo = Path.GetFileName(files[i]);
                        Bitmap orig = new Bitmap(files[i]);
                        Bitmap bmp = Bitmap(RedimensionarImagen(orig,(int)nupAncho.Value,(int)nupAlto.Value));
                        bmp.Save(NuevaCarpetaArchivo + txtNomenclatura.Text + NombreArchivo, System.Drawing.Imaging.ImagenFormat.Jpeg)
                        //barra de progreso.....
                        pbImagenes.Value = (100 * i + 1)/ (files.Length);
                     }
                }
                if(pbImagenes.Value <= 100)
                {
                    pbImagenes.Value = 0;
                    MessageBox.Show("Imagenes Procesadas");
                    Process.Start(txtRutaDestino.Text);
                }
                    

            }
        */





    }
}