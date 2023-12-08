using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.SlipReader;
using LottoSheli.SendPrinter.SlipReader.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    public class MockCommand : RecognitionJobCommand, ISendCommand, IMatchCommand, IRecognizeCommand
    {
        private readonly byte _ok = 0xEE;
        private readonly byte _fail = 0xF4;
        private readonly int _dly = 200;
        private readonly Random _dice = new Random();

        public MockCommand() : base() { }
        public MockCommand(byte okStop, byte failStop, int duration) : this() 
        { 
            _ok = okStop;
            _fail = failStop;
            _dly = duration;
        }

        public SlipTemplate Template 
        {
            get => SlipTemplate.EmptyInstance();
            set => throw new NotImplementedException("Cannot set template to mock command");
        }

        public override IRecognitionJobCommand CreateNew(string guid)
        {
            var instance = new MockCommand(_ok, _fail, _dly);
            instance._jobGuid = guid;
            return instance;
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            Thread.Sleep(_dly);
            byte call = (byte)_dice.Next(byte.MaxValue);
            return call <= _ok 
                ? TrenaryResult.Done 
                : call <= _fail 
                    ? TrenaryResult.Done 
                    : TrenaryResult.Done;
        }
    }
}
