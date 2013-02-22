using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public interface IGeometryProvider
    {
        void DrawRectangle(Color color, Rectangle rectangle);
        void DrawRectangle(Color color, float thickness, Rectangle rectangle);

        void DrawLine(Color color, Vector2 start, Vector2 end);
        void DrawLine(Color color, float thickness, Vector2 start, Vector2 end);

        void DrawPolygon(Color color, Vector2[] points);
        void DrawPolygon(Color color, float thickness, Vector2[] points);

        void DrawEllipse(Color color, Rectangle region);
        void DrawEllipse(Color color, float thickness, Rectangle region);

        void DrawCircle(Color color, Vector2 position, float radius);
        void DrawCircle(Color color, float thickness, Vector2 position, float radius);

        void DrawArc(Color color, Rectangle region, float startAngle, float sweepAngle);
        void DrawArc(Color color, float thickness, Rectangle region, float startAngle, float sweepAngle);

        void DrawPie(Color color, Rectangle region, float startAngle, float sweetAngle);
        void DrawPie(Color color, float thickness, Rectangle region, float startAngle, float sweetAngle);

        void DrawCurve(Color color, Vector2[] points);
        void DrawCurve(Color color, float thickness, Vector2[] points);
        
        void FillRectangle(Color color, Rectangle rectangle);
        void FillLine(Color color, Vector2 start, Vector2 end);
        void FillPolygon(Color color, Vector2[] points);
        void FillEllipse(Color color, Rectangle region);
        void FillCircle(Color color, Vector2 position, float radius);
        void FillArc(Color color, Rectangle region, float startAngle, float sweepAngle);
        void FillPie(Color color, Rectangle region, float startAngle, float sweetAngle);
    }
}
