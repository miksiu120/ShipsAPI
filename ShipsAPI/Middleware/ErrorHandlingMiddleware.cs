using Microsoft.AspNetCore.Http;
using ShipsAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace ShipsAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ShipNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch (InvalidPassengerException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch (TankNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch (PassengerNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch (InvalidShipOperationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch(TryFuelOverflowException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Error:{ex.Message}");

            }
            catch (WrongFuelTypeException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Error:{ex.Message}");

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
