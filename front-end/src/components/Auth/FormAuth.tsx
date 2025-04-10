import { Card } from "../ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "../ui/tabs"
import FormCadastro from "./FormCadastro"
import { FormLogin } from "./FormLogin"


export function FormAuth() {

  return (
    <Tabs defaultValue="login" className="max-w-[502px] w-full mt-6 bg-card text-primary">
      <TabsList className="grid w-full grid-cols-2">
        <TabsTrigger value="login">Já tenho Conta</TabsTrigger>
        <TabsTrigger value="cadastro">Cadastro</TabsTrigger>
      </TabsList>

      <TabsContent value="login">
        <Card className="p-6">
          <FormLogin />
        </Card>
      </TabsContent>

      <TabsContent value="cadastro">
        <Card className="p-6">
          <FormCadastro />
        </Card>
      </TabsContent>
    </Tabs>
  )
}
