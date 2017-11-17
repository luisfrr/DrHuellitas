using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class HorarioBO
    {
        public TimeSpan inicioLunes { get; set; }
        public TimeSpan finalLunes { get; set; }
        public TimeSpan inicioMarte { get; set; }
        public TimeSpan finalMartes { get; set; }
        public TimeSpan inicioMiercoles { get; set; }
        public TimeSpan finalMiercoles { get; set; }
        public TimeSpan inicioJueves { get; set; }
        public TimeSpan finalJueves { get; set; }
        public TimeSpan inicioViernes { get; set; }
        public TimeSpan finalViernes { get; set; }
        public TimeSpan inicioSabado { get; set; }
        public TimeSpan finalSabado { get; set; }
        public TimeSpan inicioDomingo { get; set; }
        public TimeSpan finalDomingo { get; set; }
        public string dia { get; set; }
    }
}