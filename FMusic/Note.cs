using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMusic
{
    public class Note
    {
        private Point _point;
        //private Point _loc;
        private int _type = 0;
        private Bitmap _img = null;

        /// <summary>
        /// Объект нота
        /// </summary>
        /// <param name="point">Точка расположения на нотном стане</param>
        /// <param name="location">Координата ноты</param>
        /// <param name="type">
        /// Тип ноты:
        /// 1 - целая
        /// 2 - половинная
        /// 3 - четвертная
        /// 4 - восьмая
        /// 5 - шестнадцатая
        /// </param>
        public Note(Point point, /*Point location,*/ int type)
        {
            _point = point;
            //_loc = location;
            _type = type;
        }

        public Point getPointNote { get { return _point; } }
        //public Point getLocationNote { get { return _loc; } }
        public int getTypeNote { get { return _type; } }

        public Bitmap getImage()
        {
            switch (_type)
            {
                case 1:
                    _img = new Bitmap(20, 20);
                    Graphics.FromImage(_img).DrawEllipse(new Pen(Color.Black, 2.0F), 0, 0, 20, 20);
                    return _img;
                default:
                    return null;
            }
        }
    }
}
