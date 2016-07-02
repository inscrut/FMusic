using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMusic
{
    public class FloppySettings
    {
        private int _id = 0;
        private int _step = 0;
        private int _dir = 0;

        /// <summary>
        /// Получить ID
        /// </summary>
        public int getID { get { return _id; } }
        /// <summary>
        /// Получить номер шагового порта
        /// </summary>
        public int getStepPin { get { return _step; } }
        /// <summary>
        /// Получить номер направляющего порта
        /// </summary>
        public int getDirPin { get { return _dir; } }

        /// <summary>
        /// Создать объект
        /// </summary>
        /// <param name="id">ID устройства</param>
        /// <param name="step">Номер порта для шага</param>
        /// <param name="dir">Номер порта для направления</param>
        public FloppySettings(int id, int step, int dir)
        {
            _id = id;
            _step = step;
            _dir = dir;
        }
    }
}
