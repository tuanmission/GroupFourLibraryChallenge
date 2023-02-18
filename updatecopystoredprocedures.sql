update BookCopy
Set CopiesAvailable=4
where BookCopyID=1;

update BookCopy
Set TotalCopies=5
where BookCopyID=3;

Select CopiesAvailable, TotalCopies
from BookCopy
where BookCopyID=3
