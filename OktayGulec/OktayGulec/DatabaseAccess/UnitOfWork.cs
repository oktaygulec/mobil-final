using OktayGulec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OktayGulec.DatabaseAccess
{
    public class UnitOfWork : IDisposable
    {
        private DBContext _context = new DBContext();

        private Manager<Hoca> _hocaManager;
        public Manager<Hoca> HocaManager 
        {
            get
            {
                if (_hocaManager == null)
                    _hocaManager = new Manager<Hoca>(_context);
                return _hocaManager;
            }
        }

        private Manager<Ders> _dersManager;
        public Manager<Ders> DersManager 
        {
            get
            {
                if (_dersManager == null)
                    _dersManager = new Manager<Ders>(_context);
                return _dersManager;
            }
        }

        private KullaniciManager _kullaniciManager;
        public KullaniciManager KullaniciManager
        {
            get
            {
                if (_kullaniciManager == null)
                    _kullaniciManager = new KullaniciManager(_context);
                return _kullaniciManager;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
            _hocaManager?.Dispose();
            _dersManager?.Dispose();
            _kullaniciManager?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
