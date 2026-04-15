from pydantic import BaseModel, field_validator

class CriarCashback(BaseModel):
    tipo_cliente: str
    valor_compra: float

    @field_validator("tipo_cliente")
    def validar_tipo_cliente(cls, v):
        tipo = v.lower().strip()
        if tipo not in ["cliente", "cliente vip"]:
            raise ValueError("Tipo de cliente inválido")
        return tipo

    @field_validator("valor_compra")
    def validar_valor_compra(cls, v):
        if v <= 0:
            raise ValueError("O valor da compra deve ser maior que zero")
        return v
    
  