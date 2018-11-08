using Newtonsoft.Json;
using System.IO;

namespace kata.GameOfLife
{
    public class JsonStringDevice : IInfrastructureDevice<CellStateEnum[,]>
    {
        protected readonly StringWriter _writer;
        protected readonly StringReader _reader;
    
        public JsonStringDevice(StringWriter writer, StringReader reader)
        {
            _writer = writer;
            _reader = reader;
        }

        public CellStateEnum[,] Input()
        {
            var json = _reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<CellStateEnum[,]>(json);
            return result;
        }

        public virtual void Output(CellStateEnum[,] state)
        {
            var result = JsonConvert.SerializeObject(state);
            // this is just for testing, will create a proper implementation of the interface later.
            _writer.Write(result);
        }
    }

    public class JsonStringDeviceForTestingGame : JsonStringDevice
    {
        public JsonStringDeviceForTestingGame(StringWriter writer, StringReader reader) : base(writer, reader)
        {
        }

        public override void Output(CellStateEnum[,] state)
        {
            var result = JsonConvert.SerializeObject(state);
            // this is just for testing, will create a proper implementation of the interface later.
            _writer.Write("#");
            _writer.Write(result);
        }
    }
}