using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core.Models;

namespace carzz.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Domain class to Api resource
            CreateMap<Photo, PhotoResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(svr => svr.Contact, opt => opt.MapFrom(v => new ContactResource{Name=v.ContactName,Phone = v.ContactPhone,Email =v.ContactEmail }))
                .ForMember(svr=>svr.Features,opt=>opt.MapFrom(v=>v.Features.Select(vf=>vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr=>vr.Make,opt=>opt.MapFrom(v=>v.Model.Make))
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v =>
                        new ContactResource {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v =>
                        v.Features.Select(vf => new KeyValuePairResource {Id = vf.Feature.Id, Name = vf.Feature.Name})));


            //Api resource to Domain class
            CreateMap<FilterResource, Filter>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v=>v.Id,opt=>opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    //Remove Feature
                    var removedFeatures= new List<VehicleFeature>();
                    foreach (var f in v.Features)
                    {
                        if(!vr.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);
                    }
                    foreach (var rf in removedFeatures)
                    {
                        v.Features.Remove(rf);
                    }
                    //Add Feature
                    foreach (var id in vr.Features)
                    {
                        if (!v.Features.Any(f => f.FeatureId == id))
                        {
                            v.Features.Add(new VehicleFeature{FeatureId = id});
                        }
                    }
                });
        }
    }
}
