program DosMain(f,Output);

var sat      : array ['A'..'P','A'..'P'] of boolean; { Matrix of who has sat with whom }
    cansit   : array ['A'..'P',1..6] of char;        { List of members each can sit with on the }
                                                     { fourth and fifth evenings }
    bound    : array ['A'..'P'] of boolean;          { Is this member bound to a seat by another ? }
    mates    : array ['A'..'P',1..3] of integer;     { Index into cansit array for fourth evening. }
                                                     { This is undefined for those members who are }
                                                     { bound by another member. }
    solution : array [1..5,1..4,1..4] of char;       { Guess what this is for... }


type string3 = string[3];  { Holds tablemates for a person }

{ InitMat initializes the matrix sat for who has sat with whom }
procedure InitMat;
var c,r : char;
begin
   for r := 'A' to 'P' do
     for c := 'A' to 'P' do
        sat[r,c] := (r = c);
end;

{ ReadData reads a data file, fills the sat matrix, and the first three lines }
{ of the solution.                                                            }
procedure ReadData(var f : text);
var 
    line,partners : string;
    night,table   : integer;
    i,j,n         : integer;
begin

   for night := 1 to 3 do begin
      Readln(f,line);
      n := 1;
      for table := 1 to 4 do begin
         while not (line[n] in ['A'..'P']) do inc(n);
         partners := Copy(line,n,4);
         for i := 1 to 3 do begin
            for j := i+1 to 4 do begin
               sat[partners[i],partners[j]] := True;
               sat[partners[j],partners[i]] := True;
            end;
         end;
         { Fill the solution array }
         for i := 1 to 4 do
            solution[night,table,i] := partners[i];
         { and move past this table }
         n := n + 4;
      end;
   end;

end;

{ FindAllowedSeating reads data from the sat matrix (who has sat with whom) and }
{ builds a list of members each member can sit with.                            }
procedure FindAllowedSeating;
var c,r : char;
    i   : integer;
begin
   for r := 'A' to 'P' do begin
     i := 1;
     for c := 'A' to 'P' do begin
        if not sat[r,c] then begin
           cansit[r,i] := c;
           inc(i);
        end;
     end;
   end;
end;

{ FindFinalSeating returns a string containing the members whom c must sit }
{ with on the fifth and final evening. This function is only defined for   }
{ those members who aren't assigned by somebody else on the fourth evening }
function FindFinalSeating(c : char): string3;
var i : integer;
    s : string;
begin
   s := '';
   for i := 1 to 6 do
      if (i <> mates[c,1]) and (i <> mates[c,2]) and (i <> mates[c,3]) then
         s := s + cansit[c,i];
   FindFinalSeating := s;
end;

{ FindSolution returns whether a solution exists or not. This is the heart }
{ of the program.                                                          }
function FindSolution: boolean;
var c,d   : char;
    ok    : boolean; { whether we have a valid configuration }
    i     : integer;
    s3,d3 : string3; { s3 is c's mates on evening 5, d3 is d's mates on same }
    found : boolean;

   { SetNextCfg rearranges the mates list for a member, i.e. picks new  }
   { tablemates for him from the cansit array. The function returns     }
   { whether such an arrangement is possible.                           }
   { This is done by moving mates[c,i] through the numbers 1..6 so that }
   { mates[c,1] < mates[c,2] < mates[c,3]. This is so that we don't try }
   { an arrangement more than once.                                     }
   function SetNextCfg(c : char): boolean;
   var ans : boolean;
   begin
      ans := True;
      if mates[c,3] = 6 then begin
         if mates[c,2] = 5 then begin
            if mates[c,1] = 4 then
               ans := False
            else begin
               inc(mates[c,1]);
               mates[c,2] := mates[c,1] + 1;
               mates[c,3] := mates[c,2] + 1;
            end
         end
         else begin
            inc(mates[c,2]);
            mates[c,3] := mates[c,2] + 1;
         end;
      end
      else
         inc(mates[c,3]);
      SetNextCfg := ans;
   end;

   { AssignTableMates updates all necessary data structures when c allocates }
   { three of his allowed mates to sit with him on the fourth evening.       }
   procedure AssignTableMates(c : char);
   var i : integer;
   begin
      { Assign tablemates seats with c }
      bound[cansit[c,mates[c,1]]] := True;
      bound[cansit[c,mates[c,2]]] := True;
      bound[cansit[c,mates[c,3]]] := True;
      { Update the sat array with new seating }
      for i := 1 to 3 do begin
         sat[c,cansit[c,mates[c,i]]] := True;
         sat[cansit[c,mates[c,i]],c] := True;
      end;
      sat[cansit[c,mates[c,1]],cansit[c,mates[c,2]]] := True;
      sat[cansit[c,mates[c,1]],cansit[c,mates[c,3]]] := True;
      sat[cansit[c,mates[c,2]],cansit[c,mates[c,3]]] := True;
      sat[cansit[c,mates[c,2]],cansit[c,mates[c,1]]] := True;
      sat[cansit[c,mates[c,3]],cansit[c,mates[c,1]]] := True;
      sat[cansit[c,mates[c,3]],cansit[c,mates[c,2]]] := True;
   end;

   { DeassignTableMates does the reverse of AssignTableMates. It is used when }
   { we have to backtrack.                                                    }
   procedure DeassignTableMates(c : char);
   var i : integer;
   begin
      { Deassign tablemates seats with c }
      bound[cansit[c,mates[c,1]]] := False;
      bound[cansit[c,mates[c,2]]] := False;
      bound[cansit[c,mates[c,3]]] := False;
      { Update the sat array with new seating }
      for i := 1 to 3 do begin
         sat[c,cansit[c,mates[c,i]]] := False;
         sat[cansit[c,mates[c,i]],c] := False;
      end;
      sat[cansit[c,mates[c,1]],cansit[c,mates[c,2]]] := False;
      sat[cansit[c,mates[c,1]],cansit[c,mates[c,3]]] := False;
      sat[cansit[c,mates[c,2]],cansit[c,mates[c,3]]] := False;
      sat[cansit[c,mates[c,2]],cansit[c,mates[c,1]]] := False;
      sat[cansit[c,mates[c,3]],cansit[c,mates[c,1]]] := False;
      sat[cansit[c,mates[c,3]],cansit[c,mates[c,2]]] := False;
   end;

   { Backtrack backs up when we're in a blind alley. Note that we have to back }
   { up over all those members who were assigned seats by another member, as   }
   { no seating data is available for them.                                    }
   procedure Backtrack;
   begin
      { Backtrack to the next unbound member and pick new tablemates    }
      { for him. Keep doing this until either we fall off the front (in }
      { which case there is no solution) or find an unbound member      }
      { that can be allocated new tablemates.                           }
      while (c >= 'A') and not ok do begin
         repeat
            dec(c);
         until (not bound[c]);
         { Unassign c's mates and find him others }
         DeassignTableMates(c);
         ok := SetNextCfg(c);
      end;
   end;

   { MoveToNextMember is used to add one more member to the solution. Note }
   { that we initialize his mates if he's not bound by someone else. This  }
   { means that if we backtrack we re-initialize all later members, which  }
   { is what we want to do.                                                }
   procedure MoveToNextMember;
   var i : integer;
   begin
      inc(c);
      if not bound[c] then
         for i := 1 to 3 do mates[c,i] := i; { Initialize tablemate pick for c }
   end;

begin
   for c := 'A' to 'P' do bound[c] := False;      { init who is assigned a seat }
   c := 'A';                                      { begin at first member }
   ok := True;                                    { we are ok at the start }
   for i := 1 to 3 do mates['A',i] := i;          { Initialize tablemate pick for 'A' }
   { main loop }
   while ok and (c < 'Q') and (c >= 'A') do begin { if we find a solution then c will go over 'P' }
                                                  { if no solution then SetNextCfg will return False }
      if not bound[c] then begin
         { Find tablemates for c: }
         { 1. Pick different tablemates while one of c's mates is bound or  }
         {    two of his mates have already sat next to one another.        }
         {    Note: We try to eliminate as many illegal possibilities as we }
         {    can here, to save time; hence the complex condition.          }
         while ok and
               (bound[cansit[c,mates[c,1]]] or
                bound[cansit[c,mates[c,2]]] or
                bound[cansit[c,mates[c,3]]] or
                sat[cansit[c,mates[c,1]],cansit[c,mates[c,2]]] or
                sat[cansit[c,mates[c,1]],cansit[c,mates[c,3]]] or
                sat[cansit[c,mates[c,2]],cansit[c,mates[c,3]]]) do
            ok := SetNextCfg(c);
         { 2. If ok then we have found tablemates for c; }
         {    else c can't sit anywhere, so we have to backtrack }
         if ok then begin
            AssignTableMates(c);
            { Make sure the fifth evening is OK }
            s3 := FindFinalSeating(c);
            ok := not (sat[c,s3[1]] or sat[c,s3[2]] or sat[c,s3[3]]);
            { Move on or backtrack according to whether fifth evening is ok. }
            { Note that if c='A' here we have backtracked as far as we can.  }
            if ok then
               MoveToNextMember
            else if c > 'A' then begin
               Backtrack;
               ok := True;
            end
            else begin
               FindSolution := False;
               ok := False;
               exit;
            end;
         end
         else if c > 'A' then begin
            Backtrack;
            ok := True;
         end
         else begin
            FindSolution := False;
            ok := False;
            exit;
         end;
      end
      else begin
         { c is then bound by another member on the fourth evening.         }
         { Check that c is correctly bound on the fifth evening. This is    }
         { done by: 1) Finding an unbound member d who has c in his cansit  }
         { list but not in his mates list (i.e. d has c in his FindFinal-   }
         { Seating list) , 2) c is then bound with d and the other members  }
         { in d's FindFinalSeating list. 3) Check that sat[c,x] is False,   }
         { where x is either d or in d´s FindFinalSeating list.             }
         d := 'A';
         found := False; { I.e. haven't found d }
         while not found and (d < c) do begin
            if not bound[d] then begin
               d3 := FindFinalSeating(d);
               if pos(c,d3) > 0 then found := True; { c sits with d on fifth }
            end;
            inc(d);
         end;
         if found then begin
            dec(d); { d was incremented once too often }
            { If found then we found d, if not then c isn't bound to anyone on the }
            { final evening (and then everything's OK). If we have d then we have  }
            { to check whether c's (and d's) mates are OK. Beware that c is in d3. }
            for i := 1 to 3 do
               if (c <> d3[i]) and sat[c,d3[i]] then
                  ok := False; { Then c can't sit with d3[i] }
         end;
         if ok then
            MoveToNextMember
         else begin
            Backtrack;
            ok := True;
         end;
      end;
   end;
   FindSolution := ok;
end;

procedure WriteSolution;
var c     : char;
    table : integer;
    last  : string3;
    i     : integer;
    done  : array ['A'..'P'] of boolean; { Needed to see who's already seated }
                                         { on the fifth evening               }
begin
   table := 1;
   for c := 'A' to 'P' do begin
      if not bound[c] then begin
         solution[4,table,1] := c;
         solution[4,table,2] := cansit[c,mates[c,1]];
         solution[4,table,3] := cansit[c,mates[c,2]];
         solution[4,table,4] := cansit[c,mates[c,3]];
         inc(table);
      end;
   end;
   for c := 'A' to 'P' do done[c] := False;
   table := 1;
   for c := 'A' to 'P' do begin
      if not done[c] then begin
         { Then c isn't assigned a seat on the fifth night - find him one }
         if not bound[c] then
            { Then we can use the FindFinalSeating function }
            last := FindFinalSeating(c)
         else begin
            { Then the mates vector isn't defined for c: Can't use the    }
            { FindFinalSeating function => We have to check whether c has }
            { sat with each member of his cansit vector.                  }
            last := '';
            for i := 1 to 6 do
                if not sat[c,cansit[c,i]] then last := last + cansit[c,i];
         end;
         solution[5,table,1] := c;
         solution[5,table,2] := last[1];
         solution[5,table,3] := last[2];
         solution[5,table,4] := last[3];
         done[last[1]] := True;
         done[last[2]] := True;
         done[last[3]] := True;
         inc(table);
      end;
   end;
end;


var
   f             : text;

procedure SolveIt;
var s : string;
    n,t,i : integer;
begin
   if FindSolution then begin
      WriteSolution;
      for n := 1 to 5 do begin
         s := '';
         for t := 1 to 4 do begin
            for i := 1 to 4 do s := s + solution[n,t,i];
            s := s + ' ';
         end;
         Writeln(s);
      end;
   end
   else
      Writeln('It is not possible to complete this schedule.');
end;

begin
   Assign(f, 'gourmet.dat');
   Reset(f);
   while not Eof(f) do
   begin
   InitMat;
   ReadData(f);
   FindAllowedSeating;
   SolveIt;
   if not Eof(f) then
      Readln(f);
   writeln;
   end;
   close(f);
end.
