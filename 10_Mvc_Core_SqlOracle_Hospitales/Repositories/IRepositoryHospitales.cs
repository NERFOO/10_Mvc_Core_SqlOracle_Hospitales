using _10_Mvc_Core_SqlOracle_Hospitales.Models;

namespace _10_Mvc_Core_SqlOracle_Hospitales.Repositories
{
    public interface IRepositoryHospitales
    {
        List<Hospital> GetHospitales();
        Hospital FindHospital(int hospitalCod);
        void CreateHospital(string nombre, string direccion, string telefono, int numCama);
        void UpdateHospital(int hospitalCod, string nombre, string direccion, string telefono, int numCama);
        void DeleteHospital(int hospitalCod);
    }
}
