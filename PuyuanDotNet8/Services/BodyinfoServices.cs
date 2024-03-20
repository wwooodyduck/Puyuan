using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
using System;

namespace PuyuanDotNet8.Services
{
    public class BodyinfoServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public BodyinfoServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> BloodPressureUpload(BodyDto Bloodpressure, string uuid)
        {
            BloodPressure bloodPressure = new BloodPressure()
            {
                Uuid = uuid,
                Systolic = Bloodpressure.systolic,
                Diastolic = Bloodpressure.diastolic,
                Pulse = Bloodpressure.pulse
            };

            _context.BloodPressure.Add(bloodPressure);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> WeightUpload(WeightDto weightdto, string uuid)
        {
            _Weight _weight = new _Weight()
            {
                Uuid = uuid,
                Weight = weightdto.weight,
                Body_Fat = weightdto.body_fat,
                Bmi = weightdto.bmi
            };

            _context._Weight.Add(_weight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> BloodSugar(BloodSugarDto bloodSugar,string uuid)
        {
            BloodSugar _bloodsugar = new BloodSugar()
            {
                Uuid = uuid,
                Sugar = bloodSugar.sugar,
                Timeperiod= bloodSugar.timeperiod
            };
            _context.BloodSugar.Add(_bloodsugar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> HbA1cUpload(HbA1cDto hbA1Cdto,string uuid)
        {
            HbA1c _hbA1C = new HbA1c()
            {
                Uuid = uuid,
                A1c= hbA1Cdto.alc
            };

            _context.HbA1c.Add(_hbA1C);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;

        }
        public async Task<IActionResult> HbA1cGet()
        {
            
        }
    }    
}
