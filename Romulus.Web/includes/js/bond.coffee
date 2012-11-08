printname = ->
  a = document.forms.wrestlerform
  a.newname.value = wrestlername()
dectohex = (a) ->
  b = a % 16
  dectohexS.charAt((a - b) / 16) + dectohexS.charAt(b)
chartoascii = (a) ->
  return 0  unless a
  chartoasciiS.indexOf(a) + 1
makerandom = ->
  makerandom.seed = (makerandom.seed * 483421 + 112930) % 928301
  returnme = makerandom.seed / 928301
wrestlername = ->
  firstpercentage = 75
  secondpercentage = 20
  adjsize = 29
  a = new Array(adjsize)
  a[0] = "Tendera"
  a[1] = "Lucy"
  a[2] = "Tamara"
  a[3] = "Cherry"
  a[4] = "Selinda"
  a[5] = "Gigi"
  a[6] = "Clarissa"
  a[7] = "Prisssy"
  a[8] = "Amish"
  a[9] = "Ivana"
  a[10] = "Candy"
  a[11] = "Malena"
  a[12] = "Kitty"
  a[13] = "Ruby"
  a[14] = "Bunny"
  a[15] = "Wynter"
  a[16] = "Tia"
  a[17] = "Asia"
  a[18] = "Sky"
  a[19] = "Regina"
  a[20] = "Sasha"
  a[21] = "Dutchess"
  a[22] = "Amber"
  a[23] = "Raven"
  a[24] = "Jasmine"
  a[25] = "Paris"
  a[26] = "Lupe"
  a[27] = "Shira"
  a[28] = "Coco"
  nounsize = 33
  b = new Array(nounsize)
  b[0] = "Goodenplenty"
  b[1] = "Loinz"
  b[2] = "Boneeya"
  b[3] = "Randy"
  b[4] = "Shagmore"
  b[5] = "Spott"
  b[6] = "Kinkart"
  b[7] = "Whetmore"
  b[8] = "Lovett"
  b[9] = "Nightlong"
  b[10] = "Starbright"
  b[11] = "Kournikova"
  b[12] = "La Rue"
  b[13] = "Highbeams"
  b[14] = "Sparks"
  b[15] = "Insatia"
  b[16] = "Swallowes"
  b[17] = "Scheetz"
  b[18] = "Rumpenstuff"
  b[19] = "Holiday"
  b[20] = "Filet"
  b[21] = "Bodaysha"
  b[22] = "Bushstorm"
  b[23] = "Fiehlguud"
  b[24] = "Beaverbanks"
  b[25] = "Marble"
  b[26] = "Wetlands"
  b[27] = "Goodwood"
  b[28] = "Lovelock"
  b[29] = "Honeytip"
  b[30] = "McCox"
  b[31] = "de Beauvoir"
  b[32] = "Menstrua"
  personsize = 6
  c = new Array(personsize)
  c[0] = "Tia"
  c[1] = "Bunny"
  c[2] = "raven"
  c[3] = "Candy"
  c[4] = "Gigi"
  c[5] = "Clarissa"
  makerandom.seed = 0
  d = document.forms.wrestlerform
  inname = d.oldname.value
  return "Enter your real name first"  if inname is ""
  i = 1
  n = inname.length
  while i <= n
    makerandom.seed += chartoascii(inname.charAt(i))
    i++
  typenum = parseInt(makerandom() * 100) + 1
  typenum = 0  if not typenum >= 1
  if typenum <= firstpercentage
    adjnum = parseInt(makerandom() * adjsize)
    adjnum = 0  if not adjnum >= 1
    nounnum = parseInt(makerandom() * nounsize)
    nounnum = 0  if not nounnum >= 1
    article = ""
    article = ""  if nounnum is 5 or nounnum is 3
    newname = article + " " + a[adjnum] + " " + b[nounnum]
  else if typenum <= firstpercentage + secondpercentage
    adjnum = parseInt(makerandom() * adjsize)
    nounnum = parseInt(makerandom() * nounsize)
    nounnum = 0  if not nounnum >= 1
    article = ""
    article = ""  if nounnum is 5 or nounnum is 3
    newname = article + " " + a[adjnum] + " " + b[nounnum]
  else
    adjnum = parseInt(makerandom() * adjsize)
    adjnum = 0  if not adjnum >= 1
    personnum = parseInt(makerandom() * personsize)
    personnum = 0  if not personnum >= 1
    newname = a[adjnum] + " " + c[personnum]
  newname
dectohexS = "0123456789ABCDEF"
chartoasciiS = ""
i = 1
while i <= 255
  chartoasciiS += unescape("%" + dectohex(i))
  i++