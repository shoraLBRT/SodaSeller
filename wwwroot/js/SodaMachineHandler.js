
let balanceCount = new Number(localStorage.getItem('balance'));
document.getElementById('balance').innerHTML = balanceCount;

let pocketMoney = new Number(localStorage.getItem('pocketMoney'));
document.getElementById('pocketMoney').innerHTML = pocketMoney;

//let change = new Number(localStorage.getItem('change'));
//document.getElementById('change').innerHTML = change;
SaveVariables();

function Incrementer(incrementcount) {
    if (pocketMoney < incrementcount)
        return alert("У вас недостаточно средств");
    balanceCount += incrementcount;
    pocketMoney -= incrementcount;
    SaveVariables();
}

function GetChange() {
    pocketMoney += balanceCount;
    balanceCount = 0;
    SaveVariables();
}
function Payment(price) {
    if (balanceCount < price)
        return alert("У вас недостаточно средств");
    balanceCount -= price;
    SaveVariables();
}
function SaveVariables() {
    localStorage.setItem('balance', balanceCount);
    //localStorage.setItem('change', change);
    localStorage.setItem('pocketMoney', pocketMoney);
    //document.getElementById('change').innerHTML = change;
    document.getElementById('pocketMoney').innerHTML = pocketMoney;
    document.getElementById('balance').innerHTML = balanceCount;
}
function Refill() {
    let refillCount = new Number(document.getElementById("refillCount").value);
    if (refillCount > 150) {
        alert("Многовато, закинем чуть поменьше");
        refillCount = 150;
    }
    pocketMoney += refillCount;
    SaveVariables();
}