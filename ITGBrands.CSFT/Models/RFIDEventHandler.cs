using Com.Zebra.Rfid.Api3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITGBrands.CSFT.Models
{
    // RFID Event Handler Class
    public class RfidEventHandler : Java.Lang.Object, IRfidEventsListener
    {
        private readonly RFIDReader _reader;
        private readonly Action<string> _updateStatus;
        public RfidEventHandler(RFIDReader reader, Action<string> updateStatus)
        { _reader = reader; _updateStatus = updateStatus; }
        public void EventReadNotify(RfidReadEvents e)
        {
            var tags = _reader.Actions.GetReadTags(100);
            if (tags != null) { foreach (var tag in tags)
                { var tagData = $"Tag ID: {tag.TagID}, RSSI: {tag.PeakRSSI}"; _updateStatus(tagData); } } }
        public void EventStatusNotify(RfidStatusEvents e)
        { 
            _updateStatus($"Status: {e.StatusEventData.StatusEventType}");
        } 
    } 
}
