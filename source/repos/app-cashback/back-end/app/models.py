from sqlalchemy import Column, Integer, String, Float, DateTime,Enum
from datetime import datetime
from app.database import Base

class Cashback(Base):
    __tablename__ = "cashbacks"

    id           = Column(Integer, primary_key=True, index=True)
    ip           = Column(String, index=True)
    tipo_cliente = Column(String, nullable=False)
    valor_compra = Column(Float, nullable=False)
    valor_cashback = Column(Float, nullable=False)
    data_cashback  = Column(DateTime, default=datetime.utcnow)

   
 