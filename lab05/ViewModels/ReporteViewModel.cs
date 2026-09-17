using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using lab05.Commands;
using lab05.Models;
using lab05.Repositories;

namespace lab05.ViewModels
{
    public class ReporteViewModel : BaseViewModel
    {
        private readonly ReporteRepository _repo;

        // --- Propiedades enlazadas a la Vista ---

        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get => _fechaInicio;
            set
            {
                _fechaInicio = value;
                OnPropertyChanged();
            }
        }

        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get => _fechaFin;
            set
            {
                _fechaFin = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReporteDetalle> _reportes;
        public ObservableCollection<ReporteDetalle> Reportes
        {
            get => _reportes;
            set
            {
                _reportes = value;
                OnPropertyChanged();
            }
        }

        // --- Comandos ---
        public ICommand GenerarCommand { get; }

        public ReporteViewModel()
        {
            _repo = new ReporteRepository();

            // Establecemos fechas por defecto (usando el año 2026 porque así están los registros de tu script SQL)
            FechaInicio = new DateTime(2026, 8, 1);
            FechaFin = new DateTime(2026, 8, 31);

            // Configuramos el comando. Solo se puede ejecutar si FechaInicio es menor o igual a FechaFin
            GenerarCommand = new RelayCommand(_ => GenerarReporte(), _ => FechaInicio <= FechaFin);

            // Cargamos el reporte por defecto al iniciar la ventana
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            // Llamamos al repositorio y actualizamos la colección que la vista está observando
            var datos = _repo.GenerarReporteFechas(FechaInicio, FechaFin);
            Reportes = new ObservableCollection<ReporteDetalle>(datos);
        }
    }
}