# Atotupe

## Short Description: 
> An application that allows you to create wallet to manage your crypto currencies

## Description
> Atotupe is an application that allows you to create wallet and add crypto currency to theme. 
Atotupe will automatically retreview current crypto currency values and calculate the total amount you have in your wallets.
\
\
It comes with a clean and easy user interface and support a few crypto currency: 
\
\- Bitcoin (BTC)
\
\- Bitcoin Cash (BCH)
\
\- Ethereum (ETH)
\
\- Litecoin (LTC)
\
\- Ripple (XRP)

## Objects
### Crypto currencies 
 - code (string)
 - name (string)
 - number (double)
 - value (double)
### Wallets
 - name (string)
 - crypto currencies ([] currency)

 ## Save look
 ### Plain look
 ```
 Wallet1
  |__ BTC (2)
  |__ BCH (4)
  |__ LTC (17)

Wallet2
  |__ BCH (3)
  |__ ETH (1)
```
### JSON look
```json
[{
    "name":"Wallet1",
    "currencies":[{
        "code":"BTC",
        "name":"Bitcoin",
        "number":2,
        "value":7000
    },{
        "code":"BCH",
        "name":"Bitcoin Cash",
        "number":4,
        "value":6000
    },{
        "code":"LTC",
        "name":"Litecoin",
        "number":17,
        "value":5000
    }]
},{
    "name":"Wallet2",
    "currencies":[{
        "code":"BCH",
        "name":"Bitcoin Cash",
        "number":3,
        "value":6000
    },{
        "code":"ETH",
        "name":"Etherum",
        "number":1,
        "value":4000
    }]
}]
```

## Maquettes
### Wallete Summary
> On Click
\
\- Update name [1]
\
\- Add currency [2]
\
\- Update currency value [3]