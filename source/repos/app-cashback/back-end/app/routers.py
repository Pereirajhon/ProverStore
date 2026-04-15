from fastapi import APIRouter, Depends, HTTPException, Request
from sqlalchemy.orm import Session

from app.database import get_db
from app.repositories import CashbackRepository
from app.services import CashbackService
from app.schemas import CriarCashback

router = APIRouter()

def get_service(db: Session = Depends(get_db)):
    repo = CashbackRepository(db)
    return CashbackService(repo)

def get_repository(db: Session = Depends(get_db)):
    return CashbackRepository(db)

@router.post("/cashback")
def gerar_cashback(
    data: CriarCashback,
    request: Request,
    cashback_service: CashbackService = Depends(get_service)
):
    ip = request.client.host or "unknown"

    try:
        return cashback_service.calcular(
            data.tipo_cliente,
            data.valor_compra,
            ip
        )
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))

from fastapi import APIRouter, Depends, HTTPException, Request

@router.get("/historico")
def historico(
    request: Request,
    ordem: str = "recent",
    cashback_repositories: CashbackRepository = Depends(get_repository)
):
    ip = request.headers.get("x-forwarded-for")
    if ip:
        ip = ip.split(",")[0].strip()
    else:
        ip = request.client.host or "unknown"

    try:
        dados = cashback_repositories.buscar_por_ip(ip, ordem)
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))

    if not dados:
        raise HTTPException(status_code=404, detail="Nenhum Cashback")

    return dados