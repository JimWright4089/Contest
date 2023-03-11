#!/usr/bin/env python3
print((lambda s:1 if "".join(reversed(s)) in (s+s) else 0)(input()))
