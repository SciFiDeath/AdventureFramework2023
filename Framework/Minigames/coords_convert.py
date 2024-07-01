s = lambda x: f"[{",".join([f"[{x.split(",")[i]},{x.split(",")[i+1]}]" for i in list(range(len(x.split(","))))[::2]])}]"

t = lambda x: [f"i:{i}, a:{a}" for i,a in list(enumerate(x.split(",")))[::2]  ]

print(t("1,2,3,4,5,6,7,8,9,10"))

print(s("1,2,3,4,5,6,7,8,9,10"))