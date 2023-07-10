using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_Helper.Library
{
    internal class AppData : IDisposable
    {
        private static readonly Lazy<AppData> _lazy = new Lazy<AppData>(() => new AppData());
        private readonly LiteDatabase _db;
        private bool disposedValue;

        public static AppData Instance { get { return _lazy.Value; } }

        private AppData()
        {
            _db = new LiteDatabase(@".AppData.db");
            Skills = _db.GetCollection<Skill>("skills");
        }

        public ILiteCollection<Skill> Skills { get; private set; }

        public Skill GetSkill(string hex)
        {
            return Skills.FindOne(s => s.Hex == hex);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    _db.Dispose();
                }

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
