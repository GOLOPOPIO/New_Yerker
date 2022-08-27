using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using New_Yerker.Model.DataBase;

namespace New_Yerker.Model
{
    public class Controller
    {
        private static PositionDB activeDb;
        public static PositionDB ActiveDb
        {
            get
            {
                if (activeDb == null)
                {
                    activeDb = new PositionDB(
                        Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), "activeDataBase.db3"));
                }
                return activeDb;
            }
        }
    }
}
