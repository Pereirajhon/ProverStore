from app.models import Cashback
from app.repositories import CashbackRepository

class CashbackService:

    def __init__(self, repo):
        self.repo = repo

    def calcular(self, tipo_cliente: str, valor_compra: float, ip: str):
        tipo = tipo_cliente.lower().strip()
        
        self._validar(tipo, valor_compra)

        cashback_valor = round(self._calcular(tipo, valor_compra), 2)

        cashback = Cashback(
            ip=ip,
            tipo_cliente=tipo,
            valor_compra=valor_compra,
            valor_cashback=cashback_valor
        )

        self.repo.registrar(cashback)

        return {
            "tipo_cliente": tipo,
            "valor_compra": valor_compra,
            "cashback": cashback_valor
        }

    def _calcular(self, tipo, valor):
        cashback = valor * 0.05  

        if tipo == "cliente vip":
            cashback += valor * 0.10 

        if valor > 500:
            cashback *= 2

        return cashback

    def _validar(self, tipo, valor):
        if tipo not in ["cliente", "cliente vip"]:
            raise ValueError("Tipo de cliente inválido")

        if not isinstance(valor, (int, float)):
            raise ValueError("O valor da compra deve ser numérico")
            
        if valor <= 0:
            raise ValueError("O valor da compra deve ser maior que zero")