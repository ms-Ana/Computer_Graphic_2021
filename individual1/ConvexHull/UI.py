from tkinter import *
from ConvexHullAndrew import andrew_convex_hull
class UI:
    def __init__(self):
        self.master = Tk()
        self.master.title("Andrew Convex Hull")
        self.master.resizable(False, False)
        self.canvas = Canvas()
        self.draw_btn = Button(self.master, text="Рисовать", fg="blue", command=self.draw_convex_hull)
        self.clear_btn = Button(self.master, text="Очистить", fg="blue", command=self.clear)
        self.canvas.bind("<Button-1>", self.draw_point)
        self.canvas.grid(row=0, column=0)
        self.draw_btn.grid(column=1, row=0)
        self.clear_btn.grid(column=2, row=0)
        self.points = []
        self.master.mainloop()

    def clear(self):
        self.canvas.delete("all")
        self.points.clear()
    def draw_convex_hull(self):
        convex_hull = andrew_convex_hull(self.points)
        self.canvas.create_line(convex_hull)


    def draw_point(self, event):
        self.points.append([event.x, event.y])
        self.canvas.create_oval(event.x, event.y, event.x+3, event.y+3,
                      fill='black')


if __name__ == '__main__':
    ui = UI()