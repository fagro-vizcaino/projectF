using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Repositories;
using System;
using System.Collections.Generic;
using ProjectF.Data.Entities.Sequences;
using ProjectF.Data.Entities.Common.ValueObjects;
using System.Linq;

namespace ProjectF.Application.NumberSequence
{
    public class DocumentNumberSequenceHandler
    {
        readonly DocumentNumberSequenceRepository _numberSequenceRepository;

        public DocumentNumberSequenceHandler(DocumentNumberSequenceRepository documentNumberSequenceRepository)
            => _numberSequenceRepository = documentNumberSequenceRepository;

        public Either<Error, NumberSequenceDto> Create(NumberSequenceDto numberSequenceDto)
            => ValidateName(numberSequenceDto)
            .Bind(Add)
            .Bind(Save);

        public Either<Error, NumberSequenceDto> Update(long id, NumberSequenceDto numberSequenceDto)
            => ValidateIsCorrectUpdate(id, numberSequenceDto)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(numberSequenceDto, c))
            .Bind(Save);

        public IEnumerable<NumberSequenceDto> GetAll()
            => _numberSequenceRepository.GetAll().Map(pt => (NumberSequenceDto)pt);

        public Either<Error, NumberSequenceDto> Find(params object[] valueKeys)
            => _numberSequenceRepository
            .Find(valueKeys)
            .Match(Some: p => Right((NumberSequenceDto)p),
                None: Left<Error, NumberSequenceDto>(Error.New("Couldn't find number sequence")));

        public Either<Error, NumberSequenceDto> Delete(long id)
           => Find(id)
             .Bind(Delete)
             .Bind(Save);
                
        public Either<Error, Unit> UpdateSequence(long id, int nextSequence)
        => _numberSequenceRepository.Find(id)
            .Match(Some: c => UpdateEntity(GetDto(c, nextSequence), c),
                None: () => Left(Error.New("Couldn't find document number sequence")))
            .Bind(Save)
            .Map(c => Unit.Default);

        NumberSequenceDto GetDto(DocumentNumberSequence numberSequence, int nextSequence)
        {
            var result = ((NumberSequenceDto)numberSequence) 
                    with { NextSequence = numberSequence.NextSequence + nextSequence };
            return result;
        }

        public Either<Error, string> GenerateSequence(long sequenceId)
            => _numberSequenceRepository.Find(new {id = sequenceId })
                .Match<Either<Error, string>>(
                    Some: c => Right($"{c.Prefix}{(c.NextSequence + 1).ToString().PadLeft(8,'0')}"),
                    None: () => Left(Error.New("Couldn't generate sequence")));

        //Missing Pagination
        Either<Error, NumberSequenceDto> ValidateIsCorrectUpdate(long id, NumberSequenceDto numberSequenceDto)
        {
            if (id == numberSequenceDto.Id) return numberSequenceDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, NumberSequenceDto> ValidateName(NumberSequenceDto numberSequenceDto)
            => Name.Of(numberSequenceDto.Name)
                .Match(Succ: c => numberSequenceDto,
                 Fail: err => Left<Error, NumberSequenceDto>(Error.New(string.Join(";", err))));

        Either<Error, NumberSequenceDto> UpdateEntity(NumberSequenceDto numberSequenceDto, DocumentNumberSequence originalDocumentSequence)
        {
            originalDocumentSequence.EditDocumentNumberSequence(new Name(numberSequenceDto.Name), 
                numberSequenceDto.Prefix,
                numberSequenceDto.InitialSequence,
                numberSequenceDto.NextSequence,
                numberSequenceDto.FinalSequence,
                numberSequenceDto.ValidUntil,
                numberSequenceDto.IsActive);

            return (NumberSequenceDto) originalDocumentSequence;
        }

        //Update next sequence only by invoice/bill
        Either<Error, NumberSequenceDto> Add(NumberSequenceDto numberSequenceDto)
        {
            try
            {
                _numberSequenceRepository.Add(numberSequenceDto);
                return numberSequenceDto;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, NumberSequenceDto> Save(NumberSequenceDto paymentTerm)
        {
            try
            {
                _numberSequenceRepository.Save();
                return paymentTerm;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, NumberSequenceDto> Delete(NumberSequenceDto numberSequence)
        {
            try
            {
                _numberSequenceRepository.Delete(numberSequence);
                return numberSequence;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

    }
}
