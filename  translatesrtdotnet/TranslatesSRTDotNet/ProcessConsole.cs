// Translate Subtitle extension .srt y .sub for .net and Mono
//
//   Copyright (C) <2010>  <Oscar Hernández Bañó>

//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.

//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TranslatesSRTDotNet
{
    class ProcessConsole : IProcess
    {
        private string mName;
        private int? mMin = 0, mMax = 0;
        private int mPosition = 0;
        private int mLongitud = 30;
        private char mCarProcess = '=';
        private char mCarProcessing = ' ';
        Timer mTimer = null;

        private void Update()
        {
            if (mMin.HasValue && mMax.HasValue)
            {
                int pLng = mLongitud * mPosition / (mMax.Value - mMin.Value + 1);
                int pLngSp = mLongitud - pLng - 1;
                string pCars = string.Empty, pCarsSp = string.Empty;
                if (pLng > 0)
                    pCars = new string(mCarProcess, pLng);
                if (pLngSp > 0)
                {
                    pCarsSp = new string(' ', pLngSp);
                    if ((int)mCarProcessing++ > 128)
                        mCarProcessing = ' ';
                }
                else
                    mCarProcessing = mCarProcess;
                Console.Write("\r{0} [{1}{2}{3}]", mName, pCars, mCarProcessing, pCarsSp);
            }
            else
                Console.Write(mName);
        }

        private void TimerCallback(Object state)
        {
            Update();
        }

        #region Miembros de IProcess

        public void Init(string argName, int? argMin, int? argMax)
        {
            mName = argName;
            mMin = argMin;
            mMax = argMax;
            Update();
            if (argMin.HasValue && argMax.HasValue)
                mTimer = new Timer(new TimerCallback(TimerCallback), null, 0, 100);
        }

        public int Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                mPosition = value;
                Update();
            }
        }

        #endregion

        #region Miembros de IProcess


        public void Done()
        {
            if (mTimer != null)
            {
                mTimer.Dispose();
                mTimer = null;
            }
            Console.WriteLine();
        }

        #endregion
    }
}
