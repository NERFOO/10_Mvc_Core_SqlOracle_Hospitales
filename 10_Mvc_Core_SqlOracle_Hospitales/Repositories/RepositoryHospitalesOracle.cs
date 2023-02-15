using _10_Mvc_Core_SqlOracle_Hospitales.Models;

namespace _10_Mvc_Core_SqlOracle_Hospitales.Repositories
{
    public class RepositoryHospitalesOracle : IRepositoryHospitales
    {

        public List<Hospital> GetHospitales()
        {
            throw new NotImplementedException();
        }

        public Hospital FindHospital(int hospitalCod)
        {
            throw new NotImplementedException();
        }
        public void CreateHospital(string nombre, string direccion, string telefono, int numCama)
        {
            throw new NotImplementedException();
        }

        public void DeleteHospital(int hospitalCod)
        {
            throw new NotImplementedException();
        }

        public void UpdateHospital(int hospitalCod, string nombre, string direccion, string telefono, int numCama)
        {
            throw new NotImplementedException();
        }
    }
}
