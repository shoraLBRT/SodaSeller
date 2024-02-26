let balanceCount = new Number(localStorage.getItem('balance'));
document.getElementById('balance').innerHTML = balanceCount;
let pocketMoney = new Number(localStorage.getItem('pocketMoney'));
document.getElementById('pocketMoney').innerHTML = pocketMoney;
let insertedCoinsDict = new Map();

// не реализовано:
// блокировка монет, блокировка товара

GetChangeFromLastSession();
FillForm();
SaveVariables();

function GetChangeFromLastSession() {
    let changeSumFromLastSession = new Number(document.getElementById("changeFromViewBag").innerHTML);
    if (changeSumFromLastSession < 0)
        alert("недостаточно средств для покупки!");
    else if (changeSumFromLastSession == 0) {
        GetChange();
    }
    else {
        SetCoinsInLocalStorageFromLastSession();
        balanceCount = changeSumFromLastSession;
        SaveVariables();
    }
}
function Incrementer(coinName, incrementcount) {
    if (pocketMoney < incrementcount)
        return alert("У вас недостаточно средств");
    balanceCount += incrementcount;
    pocketMoney -= incrementcount;
    let storageCoinCount = new Number(localStorage.getItem(coinName));
    localStorage.setItem(coinName, storageCoinCount + 1);
    FillForm();
    SaveVariables();
}
function GetChange() {
    pocketMoney += balanceCount;
    balanceCount = 0;
    ClearCoinsStorage();
    SaveVariables();
}
function ClearCoinsStorage() {
    localStorage.clear();
}
function FillForm() {
    document.getElementById("oneRubles").value = localStorage.getItem('1ruble');
    document.getElementById("twoRubles").value = localStorage.getItem('2ruble');
    document.getElementById("fiveRubles").value = localStorage.getItem('5ruble');
    document.getElementById("tenRubles").value = localStorage.getItem('10ruble');
}
function SetCoinsInLocalStorageFromLastSession() {
    localStorage.setItem('1ruble', document.getElementById("1rublesChange").innerHTML);
    localStorage.setItem('2ruble', document.getElementById("2rublesChange").innerHTML);
    localStorage.setItem('5ruble', document.getElementById("5rublesChange").innerHTML);
    localStorage.setItem('10ruble', document.getElementById("10rublesChange").innerHTML);
}
function SaveVariables() {
    localStorage.setItem('balance', balanceCount);
    localStorage.setItem('pocketMoney', pocketMoney);
    document.getElementById('pocketMoney').innerHTML = pocketMoney;
    document.getElementById('balance').innerHTML = balanceCount;
}
function RefillPocket() {
    let refillCount = new Number(document.getElementById("refillCount").value);
    if (refillCount > 150) {
        alert("Многовато, закинем чуть поменьше");
        refillCount = 150;
    }
    pocketMoney += refillCount;
    SaveVariables();
}