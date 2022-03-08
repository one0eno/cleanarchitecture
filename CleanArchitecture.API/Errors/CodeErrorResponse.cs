﻿namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } 

        public CodeErrorResponse(int statuscode, string? message = null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultMessageStatusCode(statuscode);

        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado tiene errores",
                401 => "No tienes autorizacion para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se han producido errores en el servidor",
                _ => string.Empty,
            };
        }
    }
}
