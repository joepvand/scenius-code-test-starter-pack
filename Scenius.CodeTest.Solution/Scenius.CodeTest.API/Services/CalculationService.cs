using Microsoft.EntityFrameworkCore;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.Contracts;
using Scenius.CodeTest.Contracts.Models;

namespace Scenius.CodeTest.API.Services;

public class CalculationService
{
    private readonly CalculationPublisher _calculationPublisher;
    private readonly DataContext _context;

    public CalculationService(Publishers.CalculationPublisher calculationPublisher, DataContext context)
    {
        _calculationPublisher = calculationPublisher;
        _context = context;
    }
    public async Task SubmitCalculation(string calculation)
    {
        await _calculationPublisher.SendCalculationAsync(calculation);
    }

    public async Task<IReadOnlyCollection<CalculationResults>> GetCompletedCalculations()
    {
        return await _context.CalculationResults.ToListAsync();
    }
}