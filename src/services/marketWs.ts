 import * as signalR from "@microsoft/signalr";

 let connection: signalR.HubConnection | null = null;

 class MarketWsService{
   private handlers = new Map<string, (data: any) => void>();

   async connect(){
      if (connection) return;
      connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7072/hubs/market", {
         accessTokenFactory: () => localStorage.getItem("token") || "",         
      })
      .withAutomaticReconnect()
      .build();

      await connection.start();
   }

   subscribe(
      symbol: string,
      interval: string,
      callback: (data: any) => void
   ){
      if (!connection) return;

      const key = `${symbol}_${interval}`


      if(this.handlers.has(key)) return;
      
      const handler = (data: any) =>{
         callback(data);
      };

      this.handlers.set(key, handler);
      connection.on("ReceiveCandle", handler);
      connection.invoke("Subscribe", symbol, interval);

   }
   
   unsubscribe(symbol: string, interval: string){
      if(!connection) return;

      const key = `${symbol}_${interval}`;
      const handler = this.handlers.get(key);

      if(handler){
         connection.off("ReceiveCandle", handler);
         this.handlers.delete(key);
      }
      connection.invoke("Unsubscribe", symbol, interval);
   }

   async disconnect(){
      if (!connection) return;
      await connection.stop();
      connection = null;
      this.handlers.clear();
   }
}

export const marketWs = new MarketWsService();