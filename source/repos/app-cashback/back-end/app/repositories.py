from sqlalchemy.orm import Session
from app.models import Cashback

class CashbackRepository:
    def __init__(self, db: Session):
        self.db = db

    def registrar(self, cashback: Cashback):
        self.db.add(cashback)
        self.db.commit()
        self.db.refresh(cashback)
        return cashback

    def buscar_por_ip(self, ip: str, ordem: str = "recent"):
        query = self.db.query(Cashback).filter(Cashback.ip == ip)

        if ordem == "recent":
            query = query.order_by(Cashback.data_cashback.desc())
        elif ordem == "oldest":
            query = query.order_by(Cashback.data_cashback.asc())
        else:
            raise ValueError("Ordenação inválida. Use 'recent' ou 'oldest'")

        return query.all()