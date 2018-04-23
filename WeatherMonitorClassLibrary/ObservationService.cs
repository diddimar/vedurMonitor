﻿using System;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models;

namespace WeatherMonitorClassLibrary
{
    public class ObservationService
    {
        private static Station SationError = new Station { Id="", Valid=0, Name="", Time="", Err="", Link= "", Vindhradi="", Vindstefna="Reynið aftur seinna...", MestaVindhvida="", MestiVindradi="", Hiti="Villa: Engar upplýsingar í augnablikinu", Vedurlysing="", Skyggni="", Urkoma="" };
        WindDirection _windDirection;
        public ObservationService()
        {
            _windDirection = new WindDirection();
        }
        public Station GetStationObservation(string stationId)
        {
            string path = @"http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=is&view=xml&ids=" + stationId;
            XmlDocument document = Utils.GetXmlDocument(path);
            Station response = DeserializeObservationDocument(document);

            if (response == null || !Convert.ToBoolean(response.Valid))
            { return SationError; }

            return EditStationResponse(response);
        }
        
        private Station DeserializeObservationDocument(XmlDocument doc)
        {
            Observation response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Observation));
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                response = (Observation)serializer.Deserialize(reader);
            }
            return response.Station;
        }
        private Station EditStationResponse(Station response)
        {
            response.Time = $"Klukkan: {response.Time.Substring(11).Remove(5, 3)}";
            response.Vindstefna = _windDirection.GetWindDirection(response.Vindstefna);
            response.Vindhradi = $"Vindhraði: {response.Vindhradi} m/s";
            response.Hiti = $"Hitastig: {response.Hiti} °C";
            return response;
        }

    }
}
