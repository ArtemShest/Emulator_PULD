using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace Emulator_PULD.Models
{
    public class PULD : ReactiveObject
    {

        #region private 
        public byte state;

        private bool _preparation = true; // подготовка
        private bool _ready; // готов
        private bool _turnedOff; // выключен
        private bool _turnedOn;     // включен
        private bool _overheatingLD; // перегрев ЛД
        private bool _overcurrent; // перегрузка по току
        private bool _breakage;   // обрыв цепи
        private bool _voltOverload; //перегрузка по напряжению
        private bool _shortCircuit; //короткое замыкание
        private bool _radiatorOverheating;  //перегрев радиатораТ радиатора ниже нормы
        private bool _radiatorTisBelowNormal;  //Т радиатора ниже нормы
        private bool _noConnectionToSubsystems;  //нет связи с подсистемами
        private bool _PeltierChipFailure;  // сбой микросхемы Пельтье
        private bool _overheatingPeltier;  // перегрев Пельтье
        private bool _TLDisUnstable;  // Т ЛД нестабильна
        private bool _Tstove1_isUnstable; // Т печки 1 нестабильна
        private bool _Tstove2_isUnstable; // Т печки 2 нестабильнаы
        #endregion


        #region properties
        public bool preparation
        {
            get => _preparation;
            set
            {
                state = 0x01;
                this.RaiseAndSetIfChanged(ref _preparation, value);
            }
        } // подготовка
        public bool ready
        {
            get => _ready;
            set
            {
                state = 0x02;
                this.RaiseAndSetIfChanged(ref _ready, value);
            }
        } // готов
        public bool turnedOff
        {
            get => _turnedOff;
            set
            {
                state = 0x04;
                this.RaiseAndSetIfChanged(ref _turnedOff, value);
            }
        } // выключен
        public bool turnedOn
        {
            get => _turnedOn;
            set
            {
                state = 0x05;
                this.RaiseAndSetIfChanged(ref _turnedOn, value);
            }
        } // включен
        public bool overheatingLD
        {
            get => _overheatingLD;
            set
            {
                state = 0x81;
                this.RaiseAndSetIfChanged(ref _overheatingLD, value);
            }
        } // перегрев ЛД
        public bool overcurrent
        {
            get => _overcurrent;
            set
            {
                state = 0x82; 
                this.RaiseAndSetIfChanged(ref _overcurrent, value);
            }
        }  // перегрузка по току
        public bool breakage // обрыв цепи
        {
            get => _breakage;
            set
            {
                state = 0x83; 
                this.RaiseAndSetIfChanged(ref _breakage, value);
            }
        }
        public bool voltOverload //перегрузка по напряжению
        {
            get => _voltOverload;
            set
            {
                state = 0x84;
                this.RaiseAndSetIfChanged(ref _voltOverload, value);
            }
        }
        public bool shortCircuit //короткое замыкание
        {
            get => _shortCircuit;
            set
            {
                state = 0x85; 
                this.RaiseAndSetIfChanged(ref _shortCircuit, value);
            }
        }
        public bool radiatorOverheating  //перегрев радиатора
        {
            get => _radiatorOverheating;
            set
            {
                state = 0x86;
                this.RaiseAndSetIfChanged(ref _radiatorOverheating, value);
            }
        }
        public bool radiatorTisBelowNormal   //Т радиатора ниже нормы
        {
            get => _radiatorTisBelowNormal;
            set
            {
                state = 0x87;
                this.RaiseAndSetIfChanged(ref _radiatorTisBelowNormal, value);
            }
        }
        public bool noConnectionToSubsystems   //нет связи с подсистемами
        {
            get => _noConnectionToSubsystems;
            set
            {
                state = 0x88; 
                this.RaiseAndSetIfChanged(ref _noConnectionToSubsystems, value);
            }
        }
        public bool PeltierChipFailure    // сбой микросхемы Пельтье
        {
            get => _PeltierChipFailure;
            set
            {
                state = 0x89;
                this.RaiseAndSetIfChanged(ref _PeltierChipFailure, value);
            }
        }
        public bool overheatingPeltier    //  перегрев Пельтье
        {
            get => _overheatingPeltier;
            set
            {
                state = 0x8A;
                this.RaiseAndSetIfChanged(ref _overheatingPeltier, value);
            }
        }
        public bool TLDisUnstable    //  Т ЛД нестабильна
        {
            get => _TLDisUnstable;
            set
            {
                state = 0x8B;
                this.RaiseAndSetIfChanged(ref _TLDisUnstable, value);
            }
        }
        public bool Tstove1_isUnstable    //  Т ЛД нестабильна
        {
            get => _Tstove1_isUnstable;
            set
            {
                state = 0x8C;
                this.RaiseAndSetIfChanged(ref _Tstove1_isUnstable, value);
            }
        }
        public bool Tstove2_isUnstable    //  Т ЛД нестабильна
        {
            get => _Tstove2_isUnstable;
            set
            {
                state = 0x8D;
                this.RaiseAndSetIfChanged(ref _Tstove2_isUnstable, value);
            }
        }

        #endregion

        public PULD() { }
    
    }
}
